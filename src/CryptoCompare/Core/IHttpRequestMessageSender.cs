using System;
using System.Net.Http;
using System.Threading.Tasks;

using JetBrains.Annotations;

namespace CryptoCompare
{
    public interface IHttpRequestMessageSender : IDisposable
    {
        Task<HttpResponseMessage> SendAsync([NotNull] HttpRequestMessage requestMessage);

        void SetApiKey([NotNull] string apiKey);
    }
}