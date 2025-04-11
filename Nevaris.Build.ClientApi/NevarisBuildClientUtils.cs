using System.Net.Http;

namespace Nevaris.Build.ClientApi;

internal static class NevarisBuildClientUtils
{
    public static HttpClientHandler CreateHttpClientHandler()
    {
        return new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };
    }
}