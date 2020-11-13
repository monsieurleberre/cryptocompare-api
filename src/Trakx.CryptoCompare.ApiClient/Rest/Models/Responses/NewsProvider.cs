﻿using Newtonsoft.Json;

namespace Trakx.CryptoCompare.ApiClient.Rest.Models.Responses
{
    public class NewsProvider
    {
		[JsonProperty("key")]
		public string Key { get; set; }
	    [JsonProperty("img")]
	    public string ImageUrl { get; set; }
	    [JsonProperty("lang")]
	    public string Lang { get; set; }
	    [JsonProperty("name")]
	    public string Name { get; set; }
	}
}
