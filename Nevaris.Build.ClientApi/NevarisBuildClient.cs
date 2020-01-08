using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Refit;

namespace Nevaris.Build.ClientApi
{
    /// <summary>
    /// Ermöglicht die Steuerung von NEVARIS Build (Stammdaten und Projekte) über die NEVARIS Build API.
    /// Voraussetzung ist, dass der NEVARIS Build Businessdienst auf einem erreichbaren Server läuft und
    /// so konfiguriert ist, dass die HTTP API bereitgestellt wird. Dazu muss auf dem Server in der
    /// %PROGRAMDATA%/Nemetschek/Nevaris/Nevaris.config ein entsprechender SystemSetting-Eintrag
    /// (&lt;HttpApiBaseAddress&gt;http://*:8500&lt;/HttpApiBaseAddress&gt;) vorhanden sein. Zur Kontrolle,
    /// ob die HTTP API verfügbar ist, kann vom Client aus in einem Browser über die URL [BASIS-URL]/api-docs
    /// die API-Doku abgerufen werden (z.B. http://localhost:8500/api-docs für den Fall, dass der
    /// Businessdienst auf dem lokalen Rechner läuft).
    /// </summary>
    public class NevarisBuildClient
    {
        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="hostUrl">Die Basis-URL, auf der die API bereitgestellt wird, z.B. "http://localhost:8500".</param>
        public NevarisBuildClient(string hostUrl)
        {
            HostUrl = hostUrl;
            ProjektApi = RestService.For<IProjektApi>(hostUrl, _refitSettings);
            StammApi = RestService.For<IStammApi>(hostUrl, _refitSettings);
        }

        /// <summary>
        /// Liefert die im Konstruktor übergebene Basis-URL.
        /// </summary>
        public string HostUrl { get; }

        /// <summary>
        /// Zugriff auf projektspezifische Operationen.
        /// </summary>
        public IStammApi StammApi { get; }

        /// <summary>
        /// Zugriff auf Stammdaten-Operationen.
        /// </summary>
        public IProjektApi ProjektApi { get; }

        /// <summary>
        /// Ermittelt die Version der HTTP-API und gibt sie gemeinsam mit der Version der Client-Library (Nevaris.Build.ClientApi)
        /// zurück. Auf Basis des zurückgegebenen Objekts kann überprüft werden, ob API und Client miteinander kompatibel sind.
        /// </summary>
        /// <returns>Objekt mit Versionsinformationen</returns>
        public async Task<VersionCheckResult> CheckVersion()
        {
            var apiVersionInfo = await StammApi.GetVersion();
            string[] apiVersionSegments = apiVersionInfo.ApiVersion.Split('.');

            var apiVersion = new Version(
                major: apiVersionSegments.Length > 0 ? int.Parse(apiVersionSegments[0], CultureInfo.InvariantCulture) : 0,
                minor: apiVersionSegments.Length > 1 ? int.Parse(apiVersionSegments[1], CultureInfo.InvariantCulture) : 0,
                build: apiVersionSegments.Length > 2 ? int.Parse(apiVersionSegments[2], CultureInfo.InvariantCulture) : 0,
                revision: 0);

            var clientVersion = Assembly.GetExecutingAssembly().GetName().Version;

            return new VersionCheckResult(clientVersion: clientVersion, apiVersion: apiVersion);
        }

        static RefitSettings _refitSettings = new RefitSettings
        {
            ContentSerializer = new JsonContentSerializer(new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = { new StringEnumConverter { NamingStrategy = new CamelCaseNamingStrategy() } },
                NullValueHandling = NullValueHandling.Ignore
            })
        };
    }
}
