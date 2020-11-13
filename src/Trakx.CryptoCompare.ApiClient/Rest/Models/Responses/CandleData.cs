﻿using System;
using Newtonsoft.Json;
using Trakx.CryptoCompare.ApiClient.Rest.Converters;

namespace Trakx.CryptoCompare.ApiClient.Rest.Models.Responses
{
    public class CandleData
    {
        public decimal Close { get; set; }

        public decimal High { get; set; }

        public decimal Low { get; set; }

        public decimal Open { get; set; }

        [JsonConverter(typeof(UnixTimeConverter))]
        public DateTimeOffset Time { get; set; }

        public decimal VolumeFrom { get; set; }

        public decimal VolumeTo { get; set; }
    }
}
