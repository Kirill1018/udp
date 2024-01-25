using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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

namespace Dialog
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (change_col.Text == "5")
            {
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);
                await Send(change_col.Text, socket);
                await Send(name.Text, socket);
                await Send(red_col.Text, socket);
                await Send(green_col.Text, socket);
                await Send(blue_col.Text, socket);
            }
        }

        public async Task Send(string message, Socket socket)
        {
            byte[] buffer = Encoding.Default.GetBytes(message);
            await socket.SendToAsync(new ArraySegment<byte>(buffer), SocketFlags.None, new IPEndPoint(IPAddress.Parse("192.168.1.130"), 2019));
        }
        public async Task Send(int message, Socket socket)
        {
            byte[] buffer = Encoding.Default.GetBytes(message.ToString());
            await socket.SendToAsync(new ArraySegment<byte>(buffer), SocketFlags.None, new IPEndPoint(IPAddress.Parse("192.168.1.130"), 2019));
        }
        private async void small_size_Click(object sender, RoutedEventArgs e)
        {
            int width = 3, height = 3, multiplier = 1;
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);
            await Send(width, socket);
            await Send(height, socket);
            await Send(multiplier, socket);
        }

        private async void med_size_Click(object sender, RoutedEventArgs e)
        {
            int width = 5, height = 5, multiplier = 2;
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);
            await Send(width, socket);
            await Send(height, socket);
            await Send(multiplier, socket);
        }

        private async void big_size_Click(object sender, RoutedEventArgs e)
        {
            int width = 7, height = 7, multiplier = 3;
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);
            await Send(width, socket);
            await Send(height, socket);
            await Send(multiplier, socket);
        }
    }
}