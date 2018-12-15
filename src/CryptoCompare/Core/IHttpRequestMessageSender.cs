using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CryptoCompare
{
    public interface IHttpRequestMessageSender : IDisposable
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage);

        void SetApiKey(string apiKey);
    }
}