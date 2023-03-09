using System;
using System.Collections.Generic;
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
using Servak;
using Servak.Model;
using Xceed.Wpf.Toolkit;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly myClient client;
        public bool Flag { get; set; }
        public ObjectColor ObjectColor { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            ObjectColor = new ObjectColor();
            client = new myClient();
            client.Start("127.0.0.1", 2424);
        }

        private void SendBtn_Click(object sender, RoutedEventArgs e)
        {

            client.Send(ObjectColor);


        }

        private void colPicer_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (sender is ColorPicker colorPicker)
            {
                if (ObjectColor != null)
                {
                    System.Windows.Media.Color color = colorPicker.SelectedColor ?? default(System.Windows.Media.Color);
                    ObjectColor.color = System.Drawing.Color.FromArgb(color.A, color.B, color.G, color.R);
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var ObjectList = new List<string>();
            ObjectList.Add((typeof(Button)).ToString());
            ObjectList.Add((typeof(RadioButton)).ToString());
            ObjectList.Add((typeof(CheckBox)).ToString());
            ObjectList.Add((typeof(Label)).ToString());
            ObjectList.Add((typeof(TextBlock)).ToString());
            ObjectList.Add((typeof(TextBox)).ToString());

            colorComboBox.ItemsSource = ObjectList;
        }

        private void colorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (colorComboBox.SelectedItem is string str)
            {
                ObjectColor.Name = str;
            }

        }
    }
}
