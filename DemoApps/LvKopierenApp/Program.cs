using Nevaris.Build.ClientApi;
using Newtonsoft.Json;

namespace LvKopierenApp;

/// <summary>
/// Ein Testprogramm, das den Zugriff auf Adressen über die NEVARIS Build API demonstriert.
/// </summary>
static class Program
{
    static async Task Main(string[] args)
    {
        Settings settings;

        try
        {
            Console.WriteLine("Lese Einstellungen aus Settings.json ...");
            string settingsJson = await File.ReadAllTextAsync("Settings.json");
            settings = JsonConvert.DeserializeObject<Settings>(settingsJson)
                       ?? throw new InvalidOperationException("Fehler beim Auslesen von Settings.json");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Auf Settings.json konnte nicht zugegriffen werden ({ex.GetType().Name}).");
            return;
        }

        try
        {
            string baseUrl = settings.BaseUrl ?? throw new InvalidOperationException("BaseUrl erforderlich");

            Console.WriteLine($"Stelle Verbindung zu {baseUrl} her ...");

            using var client = new NevarisBuildClient(baseUrl, new NevarisBuildClientOptions { Timeout = TimeSpan.FromSeconds(10000) });

            var versionCheckResult = await client.CheckVersion();
            if (!versionCheckResult.AreVersionsCompatible)
            {
                throw new InvalidOperationException($"Versionskonflikt: API: {versionCheckResult.ApiVersion}, client: {versionCheckResult.ClientVersion}");
            }

            var speicherorte = await client.StammApi.GetSpeicherorte();
            var speicherort = speicherorte.FirstOrDefault(s => s.Bezeichnung == settings.SpeicherortBezeichnung) 
                ?? throw new InvalidOperationException($"{settings.SpeicherortBezeichnung}: Speicherort nicht gefunden");

            speicherort = await client.StammApi.GetSpeicherort(speicherort.Id);
            var projektInfo = speicherort.ProjektInfos!.FirstOrDefault(s => s.Bezeichnung == settings.ProjektBezeichnung)
                ?? throw new InvalidOperationException($"{settings.ProjektBezeichnung}: Projekt nicht gefunden");

            var projekt = await client.ProjektApi.GetProjekt(projektInfo.Id);
     
            var quellLv = projekt.Leistungsverzeichnisse!.FirstOrDefault(l => l.Bezeichnung == settings.LvBezeichnungQuelle)
                ?? throw new InvalidOperationException($"{settings.LvBezeichnungQuelle}: LV nicht gefunden");

            // Quell-LV vollständig (d.h. mit allen Knoten und Positionen laden)
            quellLv = await client.ProjektApi.GetLeistungsverzeichnis(projekt.Id, quellLv.Id, mitKalkulationen: false);

            var zielLv = projekt.Leistungsverzeichnisse!.FirstOrDefault(l => l.Bezeichnung == settings.LvBezeichnungZiel);
            if (zielLv != null)
            {
                await client.ProjektApi.DeleteLeistungsverzeichnis(projekt.Id, zielLv.Id);
            }

            Console.WriteLine($"Erzeuge LV {settings.LvBezeichnungZiel} ...");
            
            zielLv = await client.ProjektApi.CreateLeistungsverzeichnis(
                projekt.Id,
                new NewLvInfo(quellLv)
                {
                    Bezeichnung = settings.LvBezeichnungZiel 
                });

            foreach (var quellPosition in quellLv.RootPositionen!)
            {
                await KopierePosition(
                    client.ProjektApi, 
                    projektId: projekt.Id,
                    quellPosition, 
                    zielLvId: zielLv.Id,
                    zielParentKnoten: null);
            }

            foreach (var quellKnoten in quellLv.RootKnotenListe!)
            {
                await KopiereKnoten(
                    client.ProjektApi,
                    projektId: projekt.Id,
                    quellKnoten: quellKnoten,
                    zielLvId: zielLv.Id,
                    zielParentKnoten: null);
            }
            
            Console.WriteLine($"LV {settings.LvBezeichnungZiel} wurde erzeugt.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler: {ex.Message}");
        }
    }

    static async Task KopiereKnoten(
        IProjektApi api, 
        string projektId,
        LvKnoten quellKnoten,
        Guid zielLvId,
        LvKnoten? zielParentKnoten)
    {
        Console.WriteLine($"Erzeuge LV-Knoten {quellKnoten.NummerKomplett}");

        var zielKnoten = await api.CreateLvKnoten(
            projektId,
            zielLvId,
            new NewLvKnotenInfo(quellKnoten)
            {
                ParentKnotenId = zielParentKnoten?.Id,
            });

        foreach (var quellPosition in quellKnoten.Positionen!)
        {
            await KopierePosition(api, projektId, quellPosition, zielLvId, zielKnoten);
        }

        foreach (var childQuellKnoten in quellKnoten.Knoten!)
        {
            await KopiereKnoten(
                api,
                projektId: projektId, 
                quellKnoten: childQuellKnoten, 
                zielLvId: zielLvId,
                zielParentKnoten: zielKnoten);
        }
    }
    
    static async Task KopierePosition(
        IProjektApi api,
        string projektId,
        LvPosition quellPosition,
        Guid zielLvId,
        LvKnoten? zielParentKnoten)
    {
        Console.WriteLine($"Erzeuge LV-Position {quellPosition.NummerKomplett}");
        
        await api.CreateLvPosition(
            projektId,
            zielLvId,
            new NewLvPositionInfo(quellPosition)
            {
                ParentKnotenId = zielParentKnoten?.Id,
            });
    }
}
