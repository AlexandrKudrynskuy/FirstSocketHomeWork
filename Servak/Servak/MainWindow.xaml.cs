using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
//using System.Text.Json.Serialization;
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
using Servak.Model;
using Newtonsoft.Json;
using System.Xml.Linq;
using System.Threading;

namespace Servak
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MyServer server;
        public ObjectColor ObjColor { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            server = new MyServer();
            ObjColor = new ObjectColor();
        }

        private async void colorButton_Click(object sender, RoutedEventArgs e)
        {
            //await Task.Run(async () =>
            //{

            //    ObjColor = new ObjectColor();
            //    ObjColor = await server.Receive();
            //    if (ObjColor.Name != string.Empty)
            //    {
            //        this.Dispatcher.Invoke(() =>
            //        {
            //            LogTextBlock.Text += "\n" + ObjColor.Name;
            //            LogTextBlock.UpdateLayout();
            //            foreach (var child in colorWrapPanel.Children)
            //            {
            //                MessageBox.Show(child.GetType().ToString() + "  " + ObjColor.Name);

            //                if (child.GetType().ToString() == ObjColor.Name)
            //                {
            //                    if (child is Control el)
            //                    {
            //                        el.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(ObjColor.color.A, ObjColor.color.R, ObjColor.color.G, ObjColor.color.B));
            //                        ObjColor.Name = string.Empty;
            //                    }
            //                }
            //            }

            //        });
            //    }
            //});
        }


        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {

            server.Start("127.0.0.1", 2424);
            var timer = new Timer(GetObject, null, 3, 1000);

        }

        private async void GetObject(object? state)
        {
            while (true)
            {
                await Task.Run(async () =>
                {

                    ObjColor = new ObjectColor();
                    ObjColor = await server.Receive();
                    if (ObjColor.Name != string.Empty)
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            LogTextBlock.Text += ObjColor.Name + " " + ObjColor.color + "\n";
                            LogTextBlock.UpdateLayout();
                            foreach (var child in colorWrapPanel.Children)
                            {
                            
                                if (child.GetType().ToString() == ObjColor.Name)
                                {
                                    if (child is Control el)
                                    {
                                        el.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(ObjColor.color.A, ObjColor.color.R, ObjColor.color.G, ObjColor.color.B));
                                        ObjColor.Name = string.Empty;
                                    }
                                }
                            }

                        });
                    }
                });
            }

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            server.Stop();
        }

    }
}
