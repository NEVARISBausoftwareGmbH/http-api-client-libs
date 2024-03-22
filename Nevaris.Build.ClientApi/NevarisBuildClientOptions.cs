#nullable enable
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
    /// Benutzername (notwendig, wenn der Server Authentifizierung verlangt).
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// Passwort.
    /// </summary>
    public string? Password { get; set; }
}