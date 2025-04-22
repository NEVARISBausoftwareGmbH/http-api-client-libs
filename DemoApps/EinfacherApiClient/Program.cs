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

    // Auslesen der Mandanten
    var mandanten = await client.StammApi.GetMandaten();

    foreach (var mandant in mandanten)
    {
        Console.WriteLine($"{mandant.Id}: {mandant.AnzeigeText}");
    }

    // Auslesen der Speicherorte
    var speicherorte = await client.StammApi.GetSpeicherorte();

    foreach (var speicherort in speicherorte)
    {
        Console.WriteLine($"{speicherort.Id}: {speicherort.Bezeichnung}");

        /* Ermitteln der Projekte eines Speicherorts */
        //var speicherortMiProjektInfos = await client.StammApi.GetSpeicherort(speicherort.Id, mitProjektInfos: true);
        //foreach (var projektInfo in speicherortMiProjektInfos.ProjektInfos!)
        //{
        //    Console.WriteLine($"  {projektInfo.Id}: {projektInfo.Bezeichnung}");
        //}
    }
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