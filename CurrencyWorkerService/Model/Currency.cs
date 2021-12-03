using Newtonsoft.Json;
using System.Collections.Generic;

namespace CurrencyWorkerService.Model
{
    public class Currency
    {
        [JsonProperty("currency")]
        public string Name { get; set; }
        public string Code { get; set; }
        public IList<CurrencyRate> Rates { get; set; }
    }
}
