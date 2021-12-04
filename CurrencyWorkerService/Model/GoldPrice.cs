using Newtonsoft.Json;
using System;

namespace CurrencyWorkerService.Model
{
    public class GoldPrice
    {
        [JsonProperty("cena")]
        public float Price { get; set; }
        [JsonProperty("data")]
        public DateTime Date { get; set; }
    }
}
