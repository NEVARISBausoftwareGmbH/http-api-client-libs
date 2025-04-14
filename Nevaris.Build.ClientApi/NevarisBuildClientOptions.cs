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
    /// Benutzername: Ist notwendig, wenn der Server Authentifizierung verlangt (derzeit ist das der Fall,
    /// wenn Businessdienst-seitig das Setting BuildApiAuthenticationRequired = True gesetzt ist).
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// Passwort.
    /// </summary>
    public string? Password { get; set; }
}