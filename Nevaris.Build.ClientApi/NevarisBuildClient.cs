using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Nevaris.Build.ClientApi.Fianance;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Refit;

#nullable enable
namespace Nevaris.Build.ClientApi
{
    /// <summary>
    /// Optionenobjekt, das optional an
    /// <see cref="NevarisBuildClient(string, NevarisBuildClientOptions?)"/>
    /// übergeben wird.
    /// </summary>
    public class NevarisBuildClientOptions
    {
        public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(100);
    }

    /// <summary>
    /// Ermöglicht die Steuerung von NEVARIS Build (Stammdaten und Projekte) über die NEVARIS Build RESTful API.
    /// Voraussetzung ist, dass der NEVARIS Build Businessdienst auf einem erreichbaren Server läuft und
    /// so konfiguriert ist, dass die RESTful API bereitgestellt wird. Genauere Informationen zur Installation
    /// finden Sie in README.md oder hier: https://github.com/NEVARISBausoftwareGmbH/http-api-client-libs.
    /// </summary>
    public class NevarisBuildClient : IDisposable
    {
        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="hostUrl">Die Basis-URL, auf der die API bereitgestellt wird, z.B. "http://localhost:8500".</param>
        /// <param name="options">Optionales <see cref="NevarisBuildClientOptions"/>-Objekt</param>
        public NevarisBuildClient(string hostUrl, NevarisBuildClientOptions? options = null)
        {
            options ??= new NevarisBuildClientOptions();

            HttpClient = new HttpClient
            {
                BaseAddress = new Uri(hostUrl),
                Timeout = options.Timeout
            };

            ProjektApi = RestService.For<IProjektApi>(HttpClient, _refitSettings);
            StammApi = RestService.For<IStammApi>(HttpClient, _refitSettings);
            FinanceApi = RestService.For<IFinanceApi>(HttpClient, _refitSettings);
        }

        public void Dispose()
        {
            HttpClient.Dispose();
        }

        public HttpClient HttpClient { get; }

        /// <summary>
        /// Liefert die im Konstruktor übergebene Basis-URL.
        /// </summary>
        public string HostUrl => HttpClient.BaseAddress.AbsoluteUri;

        /// <summary>
        /// Zugriff auf projektspezifische Operationen.
        /// </summary>
        public IStammApi StammApi { get; }

        /// <summary>
        /// Zugriff auf Stammdaten-Operationen.
        /// </summary>
        public IProjektApi ProjektApi { get; }

        /// <summary>
        /// Zugriff auf Finance-Operationen.
        /// </summary>
        public IFinanceApi FinanceApi { get; }

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

        static readonly RefitSettings _refitSettings = new RefitSettings
        {
            ContentSerializer = new NewtonsoftJsonContentSerializer(new JsonSerializerSettings
            {
                ContractResolver = new JsonContractResolver(),
                Converters = { new StringEnumConverter { NamingStrategy = new CamelCaseNamingStrategy() } },
                NullValueHandling = NullValueHandling.Ignore
            })
        };

        public class JsonContractResolver : CamelCasePropertyNamesContractResolver
        {
            public JsonContractResolver()
            {
                // Dictionary-Keys (z.B. für LvPosition.Preisanteile) sind case-sensitive
                // und dürfen nicht verängert werden. (Notwendig, da CamelCasePropertyNamesContractResolver
                // diese Property auf true setzt.)
                NamingStrategy!.ProcessDictionaryKeys = false;
            }
        }
    }
}
