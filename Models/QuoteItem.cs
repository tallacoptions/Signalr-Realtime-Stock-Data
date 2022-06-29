using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeStockDataUsingSignalr.Models
{
    public class QuoteItem
    {
        public string Ticker { get; set; }
        public string Data { get; set; }
    }
}
