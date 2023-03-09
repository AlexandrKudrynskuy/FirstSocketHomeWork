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
using ChatClient.Model;

namespace ChatClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MyClient myClient;
        public MainWindow()
        {
            InitializeComponent();
            myClient = new MyClient();
            myClient.Connect("127.0.0.1", 2424);
        }

        private void sendButton_Click(object sender, RoutedEventArgs e)
        {
            if (outTextBox.Text != string.Empty)
            {
                myClient.Send(outTextBox.Text);
                outTextBox.Text = string.Empty;

            }
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            var timer = new Timer(GetMessage, null, 0, 1000);

        }
        private async void GetMessage(object? state)
        {
            await myClient.Receive();
            if (myClient.Message != string.Empty)
            {
                Dispatcher.Invoke(() =>
                {
                    inTextBox.Text += "\n" + myClient.Message;
                    inTextBox.UpdateLayout();
                });
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }
    }
}
