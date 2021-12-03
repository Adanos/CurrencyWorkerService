using Newtonsoft.Json;
using System;

namespace CurrencyWorkerService.Model
{
    public class CurrencyRate
    {
        public DateTime EffectiveDate { get; set; }
        [JsonProperty("ask")]
        public float Selling { get; set; }
        [JsonProperty("bid")]
        public float Buying { get; set; }
    }
}
