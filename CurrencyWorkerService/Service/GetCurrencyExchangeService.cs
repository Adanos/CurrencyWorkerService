using CurrencyWorkerService.Model;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CurrencyWorkerService.Service
{
    public class GetCurrencyExchangeService
    {
        private readonly HttpClient _client;
        private readonly NbpApiConfiguration _configuration;
        public GetCurrencyExchangeService(IOptions<NbpApiConfiguration> configuration)
        {
            _configuration = configuration.Value;
            string addressApi = _configuration.AddressApi;
            _client = new HttpClient
            {
                BaseAddress = new Uri(addressApi)
            };
        }

        public async Task<IList<CurrencyExchangeTable>> GetCurrencyExchanges()
        {
            var currencyExchange = await _client.GetAsync(_configuration.ExchangeRatesTablePath);

            if (currencyExchange.IsSuccessStatusCode)
            {
                var result = await currencyExchange.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IList<CurrencyExchangeTable>>(result);
            }

            return null;
        }

        public async Task<Currency> GetCurrencyExchange(string currencyCode)
        {
            var currencyExchange = await _client.GetAsync(_configuration.ExchangeRatesPath + currencyCode);

            if (currencyExchange.IsSuccessStatusCode)
            {
                var result = await currencyExchange.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Currency>(result);
            }

            return null;
        }

        public async Task<GoldPrice> GetGoldPrice()
        {
            var currencyExchange = await _client.GetAsync(_configuration.GoldPricePath);

            if (currencyExchange.IsSuccessStatusCode)
            {
                var result = await currencyExchange.Content.ReadAsStringAsync();
                var goldPrices = JsonConvert.DeserializeObject<IList<GoldPrice>>(result);
                return goldPrices.FirstOrDefault();
            }

            return null;
        }
    }
}
