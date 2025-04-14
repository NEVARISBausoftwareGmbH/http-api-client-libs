using System;

namespace Nevaris.Build.ClientApi;

/// <summary>
/// Enthält Versionesinformationen zur API und zum Client. Wird von <see cref="NevarisBuildClient.CheckVersion"/> zurückgegeben.
/// </summary>
public class VersionCheckResult
{
    internal VersionCheckResult(Version clientVersion, Version apiVersion)
    {
        ClientVersion = clientVersion;
        ApiVersion = apiVersion;
    }

    /// <summary>
    /// Die Version der Nevaris.Build.ClientApi-Library.
    /// </summary>
    public Version ClientVersion { get; }

    /// <summary>
    /// Die Version der API mit der der <see cref="NevarisBuildClient"/> verbunden ist.
    /// </summary>
    public Version ApiVersion { get; }

    /// <summary>
    /// Liefert true, wenn die Major-Nummern von API und Client zusammenpassen und wenn die Minor-Nummer des Client
    /// nicht größer als die der API ist.
    /// </summary>
    public bool AreVersionsCompatible => ApiVersion.Major == ClientVersion.Major && ApiVersion.Minor >= ClientVersion.Minor;
}