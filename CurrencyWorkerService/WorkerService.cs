using CurrencyWorkerService.Model;
using CurrencyWorkerService.Service;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CurrencyWorkerService
{
    public class WorkerService : BackgroundService
    {
        private readonly string _currency;
        private readonly ILogger<WorkerService> _logger;
        private readonly GetCurrencyExchangeService _currencyExchangeService;

        public WorkerService(ILogger<WorkerService> logger, IOptions<NbpApiConfiguration> nbpApiConfiguration,
            IOptions<CurrencyConfiguration> currencyConfiguration)
        {
            _currency = currencyConfiguration.Value.Currency;
            _logger = logger;
            _currencyExchangeService = new GetCurrencyExchangeService(nbpApiConfiguration);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var goldPrice = await _currencyExchangeService.GetGoldPrice();
                _logger.LogInformation("Worker running at: {time}, date: {date}, price of gold: {goldPrice}",
                    DateTimeOffset.Now, goldPrice.Date, goldPrice.Price);
                var currencyExchanges = await _currencyExchangeService.GetCurrencyExchange(_currency);
                var firstCurrencyExchange = currencyExchanges?.Rates.FirstOrDefault();
                _logger.LogInformation("Worker running at: {time}, currency: {currency}, selling: {selling}", 
                    DateTimeOffset.Now, currencyExchanges.Name, firstCurrencyExchange.Selling);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
