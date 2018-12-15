using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using JetBrains.Annotations;

namespace CryptoCompare
{
    public class HttpRequestMessageSender : IHttpRequestMessageSender
    {
        protected readonly HttpClient _httpClient;

        public HttpRequestMessageSender([NotNull] HttpClient httpClient)
        {
            Check.NotNull(httpClient, nameof(httpClient));

            _httpClient = httpClient;
        }

        /// <inheritdoc />
        public virtual async Task<HttpResponseMessage> SendAsync([NotNull] HttpRequestMessage requestMessage)
        {
            Check.NotNull(requestMessage, nameof(requestMessage));

            return await _httpClient.SendAsync(requestMessage);
        }

        /// <inheritdoc />
        public void SetApiKey([NotNull] string apiKey)
        {
            Check.NotNullOrWhiteSpace(apiKey, nameof(apiKey));

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Apikey", apiKey);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
