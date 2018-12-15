using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CryptoCompare
{
    public class HttpRequestMessageSender : IHttpRequestMessageSender
    {
        protected readonly HttpClient _httpClient;

        public HttpRequestMessageSender(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <inheritdoc />
        public virtual async Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage)
        {
            return await _httpClient.SendAsync(requestMessage);
        }

        /// <inheritdoc />
        public void SetApiKey(string apiKey)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Apikey", apiKey);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
