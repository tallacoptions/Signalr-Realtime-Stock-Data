using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeStockDataUsingSignalr.Models
{
    public class QuoteLast
    {
        public string Ticker { get; set; }
        public string Last { get; set; }
        public string DayNetChange { get; set; }
        public string DayPercChange { get; set; }
    }
}
