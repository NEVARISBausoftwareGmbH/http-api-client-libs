#nullable enable
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.Extensions.Caching.Memory;

namespace Nevaris.Build.ClientApi;

/// <summary>
/// HttpMessageHandler, der bei jedem ausgehenden HTTP-Request aufgerufen wird. Sorgt dafür, dass ein zu den
/// Benutzerdaten passendes JWT (Json Web Token) im Header mitgegeben wird. Beim ersten Aufruf wird
/// das Token vom NEVARIS Identity Service angefordert.
/// </summary>
/// <remarks>
/// Diese Klasse wird intern von <see cref="NevarisBuildClient"/> verwendet, wenn per
/// <see cref="NevarisBuildClientOptions"/> Authentifizierungsdaten an den Konstruktor übergeben wurden.
/// Sie kann auch von Anwendungscode genutzt werden, z.B. wenn es darum geht, mehrerer HttpMessageHandlers
/// miteinander zu kombinieren.
/// </remarks>
public class NevarisIdentityHandler : DelegatingHandler
{
    private readonly string _username, _password;

    private readonly IMemoryCache _memoryCache = new MemoryCache(new MemoryCacheOptions());

    private const string TokenCacheKey = "token";

    /// <summary>
    /// Konstruktor
    /// </summary>
    /// <param name="innerHandler">Innenliegender HttpHandler. Kann eine mit new erzeugte
    /// <see cref="HttpClientHandler"/>-Instanz sein.</param>
    /// <param name="username">Benutzername zur Anmeldung am NEVARIS Identity Service</param>
    /// <param name="password">Passwort</param>
    public NevarisIdentityHandler(HttpMessageHandler innerHandler, string username, string password)
        : base(innerHandler)
    {
        _username = username;
        _password = password;
    }

    /// <inheritdoc />
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