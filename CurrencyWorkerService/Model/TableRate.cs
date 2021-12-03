using Newtonsoft.Json;

namespace CurrencyWorkerService.Model
{
    public class TableRate
    {
        [JsonProperty("currency")]
        public string Name { get; set; }
        public string Code { get; set; }
        [JsonProperty("ask")]
        public float Selling { get; set; }
        [JsonProperty("bid")]
        public float Buying { get; set; }
    }
}
