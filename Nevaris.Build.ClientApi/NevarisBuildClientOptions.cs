using System;

namespace Nevaris.Build.ClientApi;

/// <summary>
/// Optionenobjekt, das optional an
/// <see cref="NevarisBuildClient(string, NevarisBuildClientOptions?)"/>
/// übergeben wird.
/// </summary>
public class NevarisBuildClientOptions
{
    /// <summary>
    /// Timeout für Requests.
    /// </summary>
    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(100);

    /// <summary>
    /// Benutzername: Ist notwendig, wenn der Businessdienst Authentifizierung verlangt, was standardmäßig der Fall ist
    /// (Ausnahme: DisableBuildApiAuthentication = true).
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// Passwort.
    /// </summary>
    public string? Password { get; set; }
}