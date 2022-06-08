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
                new Stock("FB"),
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
            //Register NewData handler with the server
            _connection.On<StockData>(nameof(NewData), NewData);
        }

        private ObservableCollection<Stock> Stocks { get; }

        private async void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            await _connection.StartAsync();
            if (_connection.State == HubConnectionState.Connected)
            {
                //Register each stock with the server
                foreach (var s in Stocks)
                {
                    await _connection.InvokeAsync("JoinGroup", s.Ticker);
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

        private Task NewData(StockData quote)
        {
            var stock = Stocks.FirstOrDefault(s => s.Ticker.Equals(quote.Ticker, StringComparison.InvariantCultureIgnoreCase));
            if (stock != null)
            {
                stock.Last = quote.Last;
                stock.LastSize = quote.LastSize;
                stock.Bid = quote.Bid;
                stock.BidSize = quote.BidSize;
                stock.Ask = quote.Ask;
                stock.AskSize = quote.AskSize;
                stock.Volume = quote.Volume;
                stock.Open = quote.Open;
                stock.High = quote.High;
                stock.Low = quote.Low;
            }            
            return Task.CompletedTask;
        }
    }
}
