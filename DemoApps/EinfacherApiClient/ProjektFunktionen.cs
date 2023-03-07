using Nevaris.Build.ClientApi;

namespace EinfacherApiClient;

/// <summary>
/// Einzelne Hilfsfunktionen für den Zugriff auf Projekte. Demonstriert den Aufruf der API-Funktionen
/// <see cref="IStammApi.CreateProjekt"/>, <see cref="IProjektApi.UpdateProjekt"/> und
/// <see cref="IProjektApi.CreateLeistungsverzeichnis"/>. Außerdem wird der Zugriff auf Individualeigenschaften
/// eines Projekts gezeigt.
/// </summary>
public static class ProjektFunktionen
{
    /// <summary>
    /// Erzeugt ein Projekt mit einem ÖNORM-Leistungsverzeichnis. Gibt die Projekt-ID zurück.
    /// </summary>
    /// <example>
    /// <code>
    /// using var client = new NevarisBuildClient("http://localhost:8500");
    /// var speicherorte = await client.StammApi.GetSpeicherorte();
    /// var speicherort = speicherorte.Single(s => s.Bezeichnung == "Speicherort SQL Server");
    /// string projektId = await ProjektFunktionen.CreateProjekt(client.StammApi, client.ProjektApi, speicherort.Id, "2023-1", "Testprojekt");
    /// </code>
    /// </example>
    public static async Task<string> CreateProjekt(
        IStammApi stammApi,
        IProjektApi projektApi,
        Guid speicherortId,
        string? projektNummer,
        string? projektBezeichnung)
    {
        // Erstelle Projekt
        var projekt = await stammApi.CreateProjekt(speicherortId, new NewProjektInfo
        {
             Nummer = projektNummer,
             Bezeichnung = projektBezeichnung
        });

        // Erstelle Leistungsverzeichnis
        await projektApi.CreateLeistungsverzeichnis(projekt.Id, new NewLvInfo
        {
            Bezeichnung = "Leistungsverzeichnis",
            Art = LvArt.Ausschreibung,
            NormExakt = NormExakt.OENORMA2063V2021,
            Status = LvStatus.Erstellt,
            LvDetails = new LvDetails
            {
                GliederungsArt = GliederungsArt.LgGliederung,
                Währung = "EUR",
                Umsatzsteuer = "AUT20"
            }
        });

        return projekt.Id;
    }

    /// <summary>
    /// Liest einzelne Kenndaten (inklusive 2 Individualeigenschaften) eines Projekts aus und gibt sie
    /// gebündelt als <see cref="GetProjektKenndaten"/>-Objekt zurück.
    /// </summary>
    public static async Task<ProjektKenndaten> GetProjektKenndaten(IProjektApi projektApi, string projektId)
    {
        // Lese Projekt ein
        var projekt = await projektApi.GetProjekt(projektId);

        // Extrahiere die Werte der Individualeigenschaften "Region" und "Leistungsdatum", sofern diese gesetzt sind.
        // Voraussetzung ist, dass diese Individualeigenschaften in der Adminstration definiert sind.
        // (Ansonsten bleiben regionProperty und leistungsdatumProperty null).
        projekt.CustomPropertyValues.TryGetValue("Region", out var regionProperty);
        projekt.CustomPropertyValues.TryGetValue("Leistungsdatum", out var leistungsdatumProperty); 
        
        return new ProjektKenndaten
        {
            Nummer = projekt.Nummer,
            Bezeichnung = projekt.Bezeichnung,
            Status = projekt.Status,
            Region = regionProperty?.StringValue,
            Leistungsdatum = leistungsdatumProperty?.DateTimeValue
        };
    }
    
    /// <summary>
    /// Setzt einzelne Kennwerte (inklusive 2 Individualeigenschaften) eines Projekts neu.
    /// </summary>
    public static async Task UpdateProjektKenndaten(
        IProjektApi projektApi,
        string projektId,
        ProjektKenndaten kenndaten)
    {
        // Lese Projekt ein
        var projekt = await projektApi.GetProjekt(projektId);

        // Aktualisiere Eigenschaften
        projekt.Nummer = kenndaten.Nummer;
        projekt.Bezeichnung = kenndaten.Bezeichnung;
        projekt.Status = kenndaten.Status;
        projekt.CustomPropertyValues["Region"] 
            = new CustomPropertyValue { StringValue = kenndaten.Region };
        projekt.CustomPropertyValues["Leistungsdatum"] 
            = new CustomPropertyValue { DateTimeValue = kenndaten.Leistungsdatum };

        // Schreibe aktualsierte Eigenschaften zurück
        await projektApi.UpdateProjekt(projektId, projekt);
    }
    
    /// <summary>
    /// Objekt mit verschiedenen Kenndaten eines Projkets. Wird von <see cref="ProjektFunktionen.GetProjektKenndaten"/>
    /// zurückgegeben und an <see cref="ProjektFunktionen.UpdateProjektKenndaten"/> übergeben.  
    /// </summary>
    public class ProjektKenndaten
    {
        public string? Nummer { get; set; }
        
        public string? Bezeichnung { get; set; }
        
        public string? Status { get; set; }
        
        public string? Region { get; set; }
        
        public DateTime? Leistungsdatum { get; set; }
    }
}