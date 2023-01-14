using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
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
using SendImageService.Helpers;
using SendImageService.Models;

namespace SendImageService
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public List<Item> Items { get; set; } = new List<Item>();
        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            var ipAdress = IPAddress.Parse("10.2.11.28");
            var port = 27001;

            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                var endpoint = new IPEndPoint(ipAdress, port);
                socket.Bind(endpoint);
                socket.Listen(10);

               // Console.WriteLine($"Listen over : {socket.LocalEndPoint}");
                while (true)
                {
                    var client = socket.Accept();
                    Task.Run(() =>
                    {
                       // Console.WriteLine($"{client.RemoteEndPoint} connected ... ");

                        var length = 0;
                        var bytes = new byte[50000];

                        do
                        {
                            length = client.Receive(bytes);
                            var msg = Encoding.UTF8.GetString(bytes);
                            Item item = JsonHelper.GetData(msg);
                            Items.Add(item);
                          //  Console.WriteLine($"{client.RemoteEndPoint} : {msg}");
                            //if (msg == "exit")
                            //{
                            //    client.Shutdown(SocketShutdown.Both);
                            //    client.Dispose();
                            //    break;
                            //}
                        } while (true);

                    });
                }
            }
        }
    }
}
