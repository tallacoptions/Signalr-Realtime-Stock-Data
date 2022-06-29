using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.AspNetCore.SignalR.Client;

using RealtimeStockDataUsingSignalr.Models;

namespace RealtimeStockDataUsingSignalr
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly HubConnection _connection;

        public MainWindow()
        {
            InitializeComponent();

            //Create list of stocks
            Stocks = new ObservableCollection<Stock>
            {
                new Stock("GOOG"),
                new Stock("AAPL"),
                new Stock("META"),
                new Stock("AMZN"),
                new Stock("MSFT"),
                new Stock("TSLA"),
                new Stock("TWTR"),
                new Stock("INTC"),
                new Stock("PFE"),
                new Stock("SNAP")
            };

            DgStocks.ItemsSource = Stocks;

            //Create hub connection
            _connection = new HubConnectionBuilder()
                .WithUrl("https://tallacoptions.com/livehub")
                .Build();
            //Register handlers with the server
            _connection.On<QuoteItem>("Open", UpdateOpen);
            _connection.On<QuoteItem>("High", UpdateHigh);
            _connection.On<QuoteItem>("Low", UpdateLow);
            _connection.On<QuoteItem>("Volume", UpdateVolume);
            _connection.On<QuoteLast>("Last", UpdateLast);
            _connection.On<QuoteItem>("LastSize", UpdateLastSize);
            _connection.On<QuoteItem>("Bid", UpdateBid);
            _connection.On<QuoteItem>("BidSize", UpdateBidSize);
            _connection.On<QuoteItem>("Ask", UpdateAsk);
            _connection.On<QuoteItem>("AskSize", UpdateAskSize);
        }

        private ObservableCollection<Stock> Stocks { get; }

        private async void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            await _connection.StartAsync();
            if (_connection.State == HubConnectionState.Connected)
            {
                //Subscribe to data feed
                foreach (var s in Stocks)
                {
                    await _connection.InvokeAsync("JoinGroup", $"{s.Ticker}-Open");
                    await _connection.InvokeAsync("JoinGroup", $"{s.Ticker}-High");
                    await _connection.InvokeAsync("JoinGroup", $"{s.Ticker}-Low");
                    await _connection.InvokeAsync("JoinGroup", $"{s.Ticker}-Volume");
                    await _connection.InvokeAsync("JoinGroup", $"{s.Ticker}-Last");
                    await _connection.InvokeAsync("JoinGroup", $"{s.Ticker}-LastSize");
                    await _connection.InvokeAsync("JoinGroup", $"{s.Ticker}-Bid");
                    await _connection.InvokeAsync("JoinGroup", $"{s.Ticker}-BidSize");
                    await _connection.InvokeAsync("JoinGroup", $"{s.Ticker}-Ask");
                    await _connection.InvokeAsync("JoinGroup", $"{s.Ticker}-AskSize");
                }
            }
            else
            {
                MessageBox.Show("Could not connect to the server.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void BtnDisconnect_Click(object sender, RoutedEventArgs e)
        {
            await _connection.StopAsync();
        }
        
        /// <summary>
        /// Updates Open field.
        /// </summary>
        /// <param name="item">Quote data.</param>
        /// <returns></returns>
        private Task UpdateOpen(QuoteItem item)
        {
            var stock = Stocks.FirstOrDefault(s => s.Ticker.Equals(item.Ticker, StringComparison.InvariantCultureIgnoreCase));
            if(stock != null)
            {
                stock.Open = item.Data;
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Updates High field.
        /// </summary>
        /// <param name="item">Quote data.</param>
        /// <returns></returns>
        private Task UpdateHigh(QuoteItem item)
        {
            var stock = Stocks.FirstOrDefault(s => s.Ticker.Equals(item.Ticker, StringComparison.InvariantCultureIgnoreCase));
            if (stock != null)
            {
                stock.High = item.Data;
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Updates Low field.
        /// </summary>
        /// <param name="item">Quote data.</param>
        /// <returns></returns>
        private Task UpdateLow(QuoteItem item)
        {
            var stock = Stocks.FirstOrDefault(s => s.Ticker.Equals(item.Ticker, StringComparison.InvariantCultureIgnoreCase));
            if (stock != null)
            {
                stock.Low = item.Data;
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Updates Volume field.
        /// </summary>
        /// <param name="item">Quote data.</param>
        /// <returns></returns>
        private Task UpdateVolume(QuoteItem item)
        {
            var stock = Stocks.FirstOrDefault(s => s.Ticker.Equals(item.Ticker, StringComparison.InvariantCultureIgnoreCase));
            if (stock != null)
            {
                stock.Volume = item.Data;
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Updates Last field.
        /// </summary>
        /// <param name="item">Quote data.</param>
        /// <returns></returns>
        private Task UpdateLast(QuoteLast item)
        {
            var stock = Stocks.FirstOrDefault(s => s.Ticker.Equals(item.Ticker, StringComparison.InvariantCultureIgnoreCase));
            if (stock != null)
            {
                stock.Last = item.Last;
                stock.NetChange = item.DayNetChange;
                stock.PercChange = item.DayPercChange;
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Updates LastSize field.
        /// </summary>
        /// <param name="item">Quote data.</param>
        /// <returns></returns>
        private Task UpdateLastSize(QuoteItem item)
        {
            var stock = Stocks.FirstOrDefault(s => s.Ticker.Equals(item.Ticker, StringComparison.InvariantCultureIgnoreCase));
            if (stock != null)
            {
                stock.LastSize = item.Data;
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Updates Bid field.
        /// </summary>
        /// <param name="item">Quote data.</param>
        /// <returns></returns>
        private Task UpdateBid(QuoteItem item)
        {
            var stock = Stocks.FirstOrDefault(s => s.Ticker.Equals(item.Ticker, StringComparison.InvariantCultureIgnoreCase));
            if (stock != null)
            {
                stock.Bid = item.Data;
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Updates BidSize field.
        /// </summary>
        /// <param name="item">Quote data.</param>
        /// <returns></returns>
        private Task UpdateBidSize(QuoteItem item)
        {
            var stock = Stocks.FirstOrDefault(s => s.Ticker.Equals(item.Ticker, StringComparison.InvariantCultureIgnoreCase));
            if (stock != null)
            {
                stock.BidSize = item.Data;
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Updates Ask field.
        /// </summary>
        /// <param name="item">Quote data.</param>
        /// <returns></returns>
        private Task UpdateAsk(QuoteItem item)
        {
            var stock = Stocks.FirstOrDefault(s => s.Ticker.Equals(item.Ticker, StringComparison.InvariantCultureIgnoreCase));
            if (stock != null)
            {
                stock.Ask = item.Data;
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Updates AskSize field.
        /// </summary>
        /// <param name="item">Quote data.</param>
        /// <returns></returns>
        private Task UpdateAskSize(QuoteItem item)
        {
            var stock = Stocks.FirstOrDefault(s => s.Ticker.Equals(item.Ticker, StringComparison.InvariantCultureIgnoreCase));
            if (stock != null)
            {
                stock.AskSize = item.Data;
            }
            return Task.CompletedTask;
        }
    }
}
