﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Trakx.CryptoCompare.ApiClient.Rest.Exceptions;

namespace Trakx.CryptoCompare.ApiClient.Rest.Clients
{
    public interface IApiClient
    {
        /// <summary>
        /// Sends an api request asynchronously usin GET method.
        /// </summary>
        /// <param name="resourceUri">The resource uri path.</param>
        /// <returns>
        /// The asynchronous result that yields the asynchronous.
        /// </returns>
        Task<TApiResponse> GetAsync<TApiResponse>([NotNull] Uri resourceUri);

        /// <summary>
        /// Sends an api request asynchronously.
        /// </summary>
        /// <exception cref="CryptoCompareException">Thrown when a CryptoCompare api error occurs.</exception>
        /// <typeparam name="TApiResponse">Type of the API response.</typeparam>
        /// <param name="httpMethod">The HttpMethod</param>
        /// <param name="resourceUri">The resource uri path</param>
        /// <returns>
        /// The asynchronous result that yields a TApiResponse.
        /// </returns>
        Task<TApiResponse> SendRequestAsync<TApiResponse>(HttpMethod httpMethod, [NotNull] Uri resourceUri);
    }
}
