using System;
using System.Net.Http;

using CryptoCompareApi.News;

using JetBrains.Annotations;

namespace CryptoCompare
{
    /// <summary>
    /// CryptoCompare api client.
    /// </summary>
    /// <seealso cref="T:CryptoCompare.ICryptoCompareClient"/>
    public class CryptoCompareClient : ICryptoCompareClient
    {
        private static readonly Lazy<CryptoCompareClient> Lazy =
            new Lazy<CryptoCompareClient>(() => new CryptoCompareClient());

        private readonly IHttpRequestMessageSender _httpSender;

        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the CryptoCompare.CryptoCompareClient class.
        /// </summary>
        /// <param name="httpClientHandler">Custom HTTP client handler. Can be used to define proxy settigs</param>
        /// <param name="apiKey">The api key from cryptocompare</param>
        /// <param name="throttleDelayMs">Delay imposed between each queries to avoid exceeding CryptoCompare's maximum number of requests per second.</param>
        public CryptoCompareClient([NotNull] HttpClientHandler httpClientHandler, string apiKey = null, int throttleDelayMs = 0)
        {
            Check.NotNull(httpClientHandler, nameof(httpClientHandler));
            this._httpSender = throttleDelayMs <= 0
                              ? new HttpRequestMessageSender(new HttpClient(httpClientHandler, true))
                              : new ThrottledHttpRequestMessageSender(new HttpClient(httpClientHandler, true), throttleDelayMs);

            if (!string.IsNullOrWhiteSpace(apiKey))
            {
                this.SetApiKey(apiKey);
            }
        }

        /// <summary>
        /// Initializes a new instance of the CryptoCompare.CryptoCompareClient class.
        /// </summary>
        public CryptoCompareClient(string apiKey = null, int throttleDelayMs = 0)
            : this(new HttpClientHandler(), apiKey, throttleDelayMs)
        {
        }

        public void SetApiKey(string apiKey)
        {
            Check.NotNullOrWhiteSpace(apiKey, nameof(apiKey));

            this._httpSender.SetApiKey(apiKey);
        }

        /// <summary>
        /// Gets a Singleton instance of CryptoCompare api client.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static CryptoCompareClient Instance => Lazy.Value;

        /// <summary>
        /// Gets the client for coins related api endpoints.
        /// </summary>
        /// <seealso cref="P:CryptoCompare.ICryptoCompareClient.Coins"/>
        public ICoinsClient Coins => new CoinsClient(this._httpSender);

        /// <summary>
        /// Gets the client for exchanges related api endpoints.
        /// </summary>
        /// <seealso cref="P:CryptoCompare.ICryptoCompareClient.Exchanges"/>
        public IExchangesClient Exchanges => new ExchangesClient(this._httpSender);

        /// <summary>
        /// Gets the api client for market history.
        /// </summary>
        /// <seealso cref="P:CryptoCompare.ICryptoCompareClient.History"/>
        public IHistoryClient History => new HistoryClient(this._httpSender);

        /// <summary>
        /// Gets the api client for "mining" endpoints.
        /// </summary>
        /// <value>
        /// The mining client.
        /// </value>
        /// <seealso cref="P:CryptoCompare.ICryptoCompareClient.MiningClient"/>
        public IMiningClient Mining => new MiningClient(this._httpSender);

        /// <summary>
        /// Gets the api client for news endpoints.
        /// </summary>
        /// <seealso cref="P:CryptoCompare.ICryptoCompareClient.News"/>
        public INewsClient News => new NewsClient(this._httpSender);

        /// <summary>
        /// Gets the api client for cryptocurrency prices.
        /// </summary>
        /// <seealso cref="P:CryptoCompare.ICryptoCompareClient.Prices"/>
        public IPricesClient Prices => new PriceClient(this._httpSender);

        /// <summary>
        /// Gets or sets the client for api calls rate limits.
        /// </summary>
        /// <seealso cref="P:CryptoCompare.ICryptoCompareClient.RateLimits"/>
        public IRateLimitClient RateLimits => new RateLimitClient(this._httpSender);

        /// <summary>
        /// Gets the api client for "social" endpoints.
        /// </summary>
        /// <seealso cref="P:CryptoCompare.ICryptoCompareClient.Social"/>
        public ISocialStatsClient SocialStats => new SocialStatsClient(this._httpSender);

        /// <summary>
        /// The subs.
        /// </summary>
        public ISubsClient Subs => new SubsClient(this._httpSender);

        /// <summary>
        /// Gets the api client for "tops" endpoints.
        /// </summary>
        /// <seealso cref="P:CryptoCompare.ICryptoCompareClient.Tops"/>
        public ITopListClient Tops => new TopListClient(this._httpSender);

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or
        /// resetting unmanaged resources.
        /// </summary>
        /// <seealso cref="M:System.IDisposable.Dispose()"/>
        public void Dispose() => this.Dispose(true);

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or
        /// resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">True to release both managed and unmanaged resources; false to
        /// release only unmanaged resources.</param>
        internal virtual void Dispose(bool disposing)
        {
            if (this._isDisposed) return;
            if (disposing)
            {
                this._httpSender?.Dispose();
            }
            this._isDisposed = true;
        }
    }
}
