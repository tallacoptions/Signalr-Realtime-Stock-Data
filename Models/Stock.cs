using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeStockDataUsingSignalr.Models
{
    public class Stock: INotifyPropertyChanged
    {
        private string _last, _lastSize, _bid, _bidSize, _ask, _askSize, _volume, _open, _high, _low;
        public event PropertyChangedEventHandler PropertyChanged;

        public Stock(string ticker)
        {
            Ticker = ticker;
        }

        public string Ticker { get; }
        public string Last
        {
            get => _last;
            set
            {
                if (value != _last)
                {
                    _last = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string LastSize
        {
            get => _lastSize;
            set
            {
                if (value != _lastSize)
                {
                    _lastSize = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Bid
        {
            get => _bid;
            set
            {
                if (value != _bid)
                {
                    _bid = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string BidSize
        {
            get => _bidSize;
            set
            {
                if (value != _bidSize)
                {
                    _bidSize = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Ask
        {
            get => _ask;
            set
            {
                if (value != _ask)
                {
                    _ask = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string AskSize
        {
            get => _askSize;
            set
            {
                if (value != _askSize)
                {
                    _askSize = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Volume
        {
            get => _volume;
            set
            {
                if (value != _volume)
                {
                    _volume = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Open
        {
            get => _open;
            set
            {
                if (value != _open)
                {
                    _open = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string High
        {
            get => _high;
            set
            {
                if (value != _high)
                {
                    _high = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Low
        {
            get => _low;
            set
            {
                if (value != _low)
                {
                    _low = value;
                    NotifyPropertyChanged();
                }
            }
        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
