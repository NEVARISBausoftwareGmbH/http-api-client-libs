#nullable enable
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Nevaris.Build.ClientApi.Fianance;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Refit;
using IdentityModel.Client;
using Microsoft.Extensions.Caching.Memory;

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
            httpMessageHandler = new AuthenticatedHttpClientHandler(
                new HttpClientHandler(), options.Username, options.Password);
        }
        else
        {
            httpMessageHandler = new HttpClientHandler();
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

    /// <summary>
    /// Handler, der bei jedem ausgehenden HTTP-Request aufgerufen wird. Sorgt dafür, dass ein zu den
    /// Benutzerdaten passendes JWT (Json Web Token) im Header mitgegeben wird. Beim ersten Aufruf wird
    /// das Token vom NEVARS Identity Service angefordert. 
    /// </summary>
    private class AuthenticatedHttpClientHandler : DelegatingHandler
    {
        private readonly string _username, _password;

        private readonly IMemoryCache _memoryCache = new MemoryCache(new MemoryCacheOptions());

        private const string TokenCacheKey = "token";

        public AuthenticatedHttpClientHandler(HttpMessageHandler innerHandler, string username, string password)
            : base(innerHandler)
        {
            _username = username;
            _password = password;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var token = await GetToken(cancellationToken).ConfigureAwait(false);
            
            // Token in Header aufnehmen 
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }

        private async ValueTask<string> GetToken(CancellationToken cancellationToken)
        {
            if (_memoryCache.Get<string>(TokenCacheKey) is { } cachedToken)
            {
                return cachedToken;
            }

            using var client = new HttpClient();

            var discoveryDocument = await client.GetDiscoveryDocumentAsync(
                "https://nevarisidentityservice.azurewebsites.net", cancellationToken).ConfigureAwait(false);

            // Token vom NEVARIS Identity Server anfordern
            var response = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = discoveryDocument.TokenEndpoint,
                ClientId = "password.client",
                UserName = _username,
                Password = _password,
                Scope = "openid profile backend"
            }, cancellationToken).ConfigureAwait(false);

            var token = response.AccessToken ?? throw new AuthenticationException("Missing JWT");

            // Token in Cache speichern, damit nicht bei jedem Request ein neues angefordert werden muss.
            // Wir sorgen dafür, dass kurz vor Ablauf der "expiration time" des Tokens der Cache-Eintrag
            // geleert wird.
            using var entry = _memoryCache.CreateEntry(TokenCacheKey);
            entry.Value = token;
            entry.AbsoluteExpiration = GetTokenExpirationTime(token).Subtract(TimeSpan.FromMinutes(2));

            return token;
        }

        /// <summary>
        /// Extrahiert die "expiration time" aus einem JWT.
        /// </summary>
        private static DateTimeOffset GetTokenExpirationTime(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            var tokenExp = jwtSecurityToken.Claims.First(claim => claim.Type.Equals("exp")).Value;
            var ticks = long.Parse(tokenExp);
            return DateTimeOffset.FromUnixTimeSeconds(ticks);
        }
    }
}