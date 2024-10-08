using Nevaris.Build.ClientApi;

try
{
    // Verbindung zur RESTful API herstellen. Dies setzt voraus, dass auf dem lokalen Rechner
    // der NEVARIS Build Businessdienst läuft und die API hostet. Der erste Zugriff erfolgt
    // erst beim ersten API-Aufruf (hier: client.StammApi.GetSpeicherorte()).
    using var client = new NevarisBuildClient("https://localhost:8500", new NevarisBuildClientOptions
    {
        // Falls Businessdienst-seitig das Setting BuildApiAuthenticationRequired = True gesetzt ist,
        // müssen hier die Authentifizierungsinformationen übergeben werden.
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

    // Auslesen der Speicherorte
    var speicherorte = await client.StammApi.GetSpeicherorte();

    foreach (var speicherort in speicherorte)
    {
        Console.WriteLine($"{speicherort.Id}: {speicherort.Bezeichnung}");
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