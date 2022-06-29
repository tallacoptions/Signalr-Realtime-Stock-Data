using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeStockDataUsingSignalr.Models
{
    public class StockData
    {
        public string Ticker { get; set; }
        public string Last { get; set; }
        public string LastSize { get; set; }
        public string Bid { get; set; }
        public string BidSize { get; set; }
        public string Ask { get; set; }
        public string AskSize { get; set; }
        public string Volume { get; set; }
        public string Open { get; set; }
        public string High { get; set; }
        public string Low { get; set; }
    }
}
