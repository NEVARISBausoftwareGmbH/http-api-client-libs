#nullable enable
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace Nevaris.Build.ClientApi;

/// <summary>
/// HttpMessageHandler, der bei jedem ausgehenden HTTP-Request aufgerufen wird. Sorgt dafür, dass ein zu den
/// Benutzerdaten passendes JWT (Json Web Token) im Header mitgegeben wird. Beim ersten Aufruf wird
/// das Token vom API-Endpunkt /authentication/login angefordert.
/// </summary>
/// <remarks>
/// Diese Klasse wird intern von <see cref="NevarisBuildClient"/> verwendet, wenn per
/// <see cref="NevarisBuildClientOptions"/> Authentifizierungsdaten an den Konstruktor übergeben wurden.
/// Sie kann auch von Anwendungscode genutzt werden, z.B. wenn es darum geht, mehrerer HttpMessageHandlers
/// miteinander zu kombinieren.
/// </remarks>
public class NevarisIdentityHandler : DelegatingHandler
{
    private readonly string _hostUrl, _username, _password;

    private readonly IMemoryCache _memoryCache = new MemoryCache(new MemoryCacheOptions());

    private const string TokenCacheKey = "token";

    /// <summary>
    /// Konstruktor
    /// </summary>
    /// <param name="innerHandler">Innenliegender HttpHandler. Kann eine mit new erzeugte
    /// <see cref="HttpClientHandler"/>-Instanz sein.</param>
    /// <param name="hostUrl">API-Basis-URL</param>
    /// <param name="username">Benutzername zur Anmeldung am NEVARIS Identity Service</param>
    /// <param name="password">Passwort</param>
    public NevarisIdentityHandler(HttpMessageHandler innerHandler, string hostUrl, string username, string password)
        : base(innerHandler)
    {
        _hostUrl = hostUrl;
        _username = username;
        _password = password;
    }

    /// <inheritdoc />
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var token = await GetToken().ConfigureAwait(false);

        // Token in Header aufnehmen 
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _memoryCache.Dispose();
        }

        base.Dispose(disposing);
    }

    private async ValueTask<string> GetToken()
    {
        if (_memoryCache.Get<string>(TokenCacheKey) is { } cachedToken)
        {
            return cachedToken;
        }

        using var client = new HttpClient(NevarisBuildClientUtils.CreateHttpClientHandler());
        client.BaseAddress = new Uri(_hostUrl);

        // Token von API-Endpunkt /authentication/login anfordern 
        var token = await client.GetStringAsync(
                $"authentication/login?userName={Uri.EscapeDataString(_username)}&password={Uri.EscapeDataString(_password)}")
            .ConfigureAwait(false) ?? throw new AuthenticationException("Missing JWT");

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