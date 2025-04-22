using Nevaris.Build.ClientApi;

try
{
    // Verbindung zur RESTful API herstellen. Dies setzt voraus, dass auf dem lokalen Rechner
    // der NEVARIS Build Businessdienst läuft und die API hostet. Der erste Zugriff erfolgt
    // erst beim ersten API-Aufruf (hier: client.StammApi.GetSpeicherorte()).
    using var client = new NevarisBuildClient("https://localhost:8500", new NevarisBuildClientOptions
    {
        // Hier müssen Authentifizierungsdaten übergeben werden, außer die API wird per Build
        // gehostet oder DisableBuildApiAuthentication = true gesetzt.
        // Username = "<Username>",
        // Password = "<Password>"
    });

    // API-Version abfragen und auf Kompatibilität mit Client überprüfen
    var versionCheckResult = await client.CheckVersion();
    if (!versionCheckResult.AreVersionsCompatible)
    {
        throw new InvalidOperationException(
            $"Versionskonflikt: API: {versionCheckResult.ApiVersion}, client: {versionCheckResult.ClientVersion}");
    }

    /* Das benötigte Projekt suchen */
    var speicherorte = await client.StammApi.GetSpeicherorte();
    var speicherort = speicherorte.FirstOrDefault() ?? throw new InvalidOperationException("Kein Speicherort gefunden");
    var speicherortMitProjektInfos = await client.StammApi.GetSpeicherort(speicherort.Id, mitProjektInfos: true);
    var projektInfo = speicherortMitProjektInfos.ProjektInfos?.FirstOrDefault(_=>_.Bezeichnung == "Test Projekt");
    if (projektInfo == null)
    {
        Console.WriteLine("Das gewünschte Projekt wurde im Speicherort nicht gefunden!");
        return;
    }

    var projekt = await client.ProjektApi.GetProjekt(projektInfo.Id);
    var lv = await client.ProjektApi.GetLeistungsverzeichnis(projekt.Id, projekt.Leistungsverzeichnisse!.First().Id, mitKalkulationen: true);

    if(lv == null)
    {
        Console.WriteLine("Das gewünschte Leistungsverzeichnis wurde im Projekt nicht gefunden!");
        return;
    }
    
    var kalkulation = lv.RootKalkulationen!.FirstOrDefault();
    if(kalkulation == null)
    {
        Console.WriteLine("Für das LV existiert keine Kalkulation");
        return;
    }

    /* Das zu anzupassende Material ermitteln */
    var material = await client.ProjektApi.GetBetriebsmittelByNummer(projekt.Id, "M01", projekt.Guid);

    if(material == null)
    {
        Console.WriteLine("Das gewünschte Material wurde im Projekt nicht gefunden!");
        return;
    }

    await client.ProjektApi.UpdateBetriebsmittel(
        projekt.Id,
        material.Id, 
        new Betriebsmittel()
        {
            Id = material.Id,
            Art = BetriebsmittelArt.Material,
            Nummer =  "01",
            Einheit = "t",
            Kosten = new List<BetriebsmittelKosten>()
            {
                new BetriebsmittelKosten()
                {
                    MaterialDetails = new BetriebsmittelKostenMaterialDetails()
                    {
                        Listenpreis = new Money("EUR", 100m),       //Listenpreis
                        Rabatt = 5,                                 //Rabatt
                    },
                    KostenebeneId = kalkulation.Id, 
                }
            }
        });

    /* Hole das Betriebsmittel Material erneut ab und gib die Kosten aus */
    material = await client.ProjektApi.GetBetriebsmittelByNummer(projekt.Id, "M01");
    var kostenOfMaterial = material.Kosten?.FirstOrDefault();

    if (kostenOfMaterial?.MaterialDetails?.Listenpreis == null) { throw new InvalidOperationException("Die Kosten müssen existieren, da diese gerade aktualisiert wurden."); }

    Console.WriteLine($"Betriebsmittel: {material.Bezeichnung}");
    Console.WriteLine($"Listenpreis: {kostenOfMaterial.MaterialDetails.Listenpreis[0].Value}{kostenOfMaterial.MaterialDetails.Listenpreis[0].Currency}");
}
catch (Refit.ApiException ex)
{
    Console.WriteLine(ex.Message);
    Console.WriteLine(ex.Content);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}