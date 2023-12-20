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
using System.Windows.Shapes;

namespace WpfApp1.Views
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        public static Main main;
        public Main()
        {
            InitializeComponent();
            main = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new SettingPage();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new AddUser();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new HomePage();
        }

        public void ChangeImage(Uri newImageUri)
        {
            var imageBrush = new ImageBrush(new BitmapImage(new Uri("E:\\РПМ\\УП_ПРАКТИКА\\WpfApp1\\WpfApp1\\Picture\\lightfon.jpg", UriKind.Relative)));
            this.Background = imageBrush;
        }
    }
}
