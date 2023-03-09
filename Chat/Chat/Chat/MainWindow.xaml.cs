using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using ServerChat.Model;

namespace Chat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MyServer myServer;
        public MainWindow()
        {
            InitializeComponent();
            myServer = new MyServer();  
            myServer.Start("127.0.0.1", 2424);
        }

        private void sendButton_Click(object sender, RoutedEventArgs e)
        {
            if (outTextBox.Text != string.Empty)
            {
                myServer.Send(outTextBox.Text);
                outTextBox.Text = string.Empty;
            }

        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
                var Timer = new Timer(GetMessage, null, 0, 1000);
           
        }

        private async void GetMessage(object? state)
        {
            string res = await myServer.ReceiveAsync();
            if (res != string.Empty)
            {
                Dispatcher.Invoke(() =>
                {
                    inTextBox.Text += "\n" + res;
                    inTextBox.UpdateLayout();
                    res = string.Empty;
                });
            }
        }
    }
}
