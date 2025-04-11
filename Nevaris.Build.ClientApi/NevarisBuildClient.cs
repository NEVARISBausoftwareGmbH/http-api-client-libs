#nullable enable
using System;
using System.Globalization;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Nevaris.Build.ClientApi.Fianance;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Refit;

namespace Nevaris.Build.ClientApi;

/// <summary>
/// Ermöglicht die Steuerung von NEVARIS Build (Stammdaten und Projekte) über die NEVARIS Build RESTful API.
/// Voraussetzung ist, dass der NEVARIS Build Businessdienst auf einem erreichbaren Server läuft und
/// so konfiguriert ist, dass die RESTful API bereitgestellt wird. Genauere Informationen zur Installation
/// finden Sie in README.md oder hier: https://github.com/NEVARISBausoftwareGmbH/http-api-client-libs.
/// </summary>
public class NevarisBuildClient : IDisposable
{
    /// <summary>
    /// Gibt an, ob in <see cref="Dispose"/> der zugrundeliegende <see cref="HttpClient"/> geschlossen werden soll.
    /// </summary>
    private readonly bool _keepHttpClientOpen;

    /// <summary>
    /// Konstruktor.
    /// </summary>
    /// <param name="hostUrl">Die Basis-URL, auf der die API bereitgestellt wird, z.B. "http://localhost:8500".</param>
    /// <param name="options">Optionales <see cref="NevarisBuildClientOptions"/>-Objekt</param>
    public NevarisBuildClient(string hostUrl, NevarisBuildClientOptions? options = null)
        : this(CreateHttpClient(hostUrl, options ?? new NevarisBuildClientOptions()), keepHttpClientOpen: false)
    {
    }

    /// <summary>
    /// Alternativer Konstruktor, der direkt einen <see cref="HttpClient"/> entgegennimmt.
    /// </summary>
    /// <param name="httpClient">Der zugrundeliegende <see cref="HttpClient"/></param>
    /// <param name="keepHttpClientOpen">Falls true (= Default), bleibt der übergebene HttpClient auch
    /// nach dem <see cref="Dispose"/>-Aufruf geöffnet, ansonsten wird er geschlossen.</param>
    public NevarisBuildClient(HttpClient httpClient, bool keepHttpClientOpen = true)
    {
        HttpClient = httpClient;
        _keepHttpClientOpen = keepHttpClientOpen;

        ProjektApi = RestService.For<IProjektApi>(HttpClient, _refitSettings);
        StammApi = RestService.For<IStammApi>(HttpClient, _refitSettings);
        FinanceApi = RestService.For<IFinanceApi>(HttpClient, _refitSettings);
    }

    /// <summary>
    /// Schließt den <see cref="HttpClient"/> (d.h. die zugrundeliegende HTTP-Verbindung zum Server).
    /// </summary>
    public void Dispose()
    {
        if (!_keepHttpClientOpen)
        {
            HttpClient.Dispose();
        }
    }

    /// <summary>
    /// Der zugrundeliegende <see cref="HttpClient"/>.
    /// </summary>
    public HttpClient HttpClient { get; }

    /// <summary>
    /// Liefert die im Konstruktor übergebene Basis-URL.
    /// </summary>
    public string? HostUrl => HttpClient.BaseAddress?.AbsoluteUri;

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

        var clientVersion = Assembly.GetExecutingAssembly().GetName().Version
            ?? throw new InvalidOperationException("assembly version is null");

        return new VersionCheckResult(clientVersion: clientVersion, apiVersion: apiVersion);
    }

    private static readonly RefitSettings _refitSettings = new RefitSettings
    {
        ContentSerializer = new NewtonsoftJsonContentSerializer(new JsonSerializerSettings
        {
            ContractResolver = new JsonContractResolver(),
            Converters = { new StringEnumConverter { NamingStrategy = new CamelCaseNamingStrategy() } },
            NullValueHandling = NullValueHandling.Ignore
        })
    };

    private static HttpClient CreateHttpClient(string hostUrl, NevarisBuildClientOptions options)
    {
        HttpMessageHandler httpMessageHandler;

        if (options.Username is not null && options.Password is not null)
        {
            httpMessageHandler = new NevarisIdentityHandler(
                NevarisBuildClientUtils.CreateHttpClientHandler(),
                hostUrl,
                options.Username,
                options.Password);
        }
        else
        {
            httpMessageHandler = NevarisBuildClientUtils.CreateHttpClientHandler();
        }

        return new HttpClient(httpMessageHandler)
        {
            BaseAddress = new Uri(hostUrl),
            Timeout = options.Timeout
        };
    }

    private class JsonContractResolver : CamelCasePropertyNamesContractResolver
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