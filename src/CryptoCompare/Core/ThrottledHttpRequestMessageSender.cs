using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using JetBrains.Annotations;

namespace CryptoCompare
{
    public class ThrottledHttpRequestMessageSender : HttpRequestMessageSender
    {
        private readonly SemaphoreSlim _semaphore;
        private int _millisecondsDelay;

        public ThrottledHttpRequestMessageSender([NotNull] HttpClient httpClient, int millisecondsDelay = 100)
            :base(httpClient)
        {
            _millisecondsDelay = (int)millisecondsDelay;
            _semaphore = new SemaphoreSlim(1, 1);
        }

        public override async Task<HttpResponseMessage> SendAsync([NotNull] HttpRequestMessage requestMessage)
        {
            Check.NotNull(requestMessage, nameof(requestMessage));

            await _semaphore.WaitAsync();
            try
            {
                return await _httpClient.SendAsync(requestMessage);
            }
            finally
            {
                await Task.Delay(_millisecondsDelay);
                _semaphore.Release(1);
            }
        }
    }
}