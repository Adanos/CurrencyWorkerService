using System;
using System.Collections.Generic;

namespace CurrencyWorkerService.Model
{
    public class CurrencyExchangeTable
    {
        public string Table { get; set; }
        public DateTime TradingDate { get; set; }
        public DateTime EffectiveDate { get; set; }
        public IList<TableRate> Rates { get; set; }
    }
}
