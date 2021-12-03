using CurrencyWorkerService.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CurrencyWorkerService.Service
{
    public class GetCurrencyExchangeService
    {
        private readonly HttpClient _client;
        public GetCurrencyExchangeService()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("http://api.nbp.pl/")
            };
        }

        public async Task<IList<CurrencyExchangeTable>> GetCurrencyExchanges()
        {
            var currencyExchange = await _client.GetAsync("api/exchangerates/tables/c/");

            if (currencyExchange.IsSuccessStatusCode)
            {
                var result = await currencyExchange.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IList<CurrencyExchangeTable>>(result);
            }

            return null;
        }

        public async Task<Currency> GetCurrencyExchange(string currencyCode)
        {
            var currencyExchange = await _client.GetAsync("api/exchangerates/rates/c/" + currencyCode);

            if (currencyExchange.IsSuccessStatusCode)
            {
                var result = await currencyExchange.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Currency>(result);
            }

            return null;
        }
    }
}
