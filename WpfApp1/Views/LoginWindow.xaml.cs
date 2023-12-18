using MaterialDesignThemes.Wpf;
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
using WpfApp1.ViewModels;

namespace WpfApp1.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {

        LoginViewModel viewModel;

        public LoginWindow()
        {
            InitializeComponent();
            var imageBrush = new ImageBrush(new BitmapImage(new Uri("E:\\РПМ\\УП_ПРАКТИКА\\WpfApp1\\WpfApp1\\Picture\\lightfon.jpg", UriKind.Relative)));
            this.Background = imageBrush;
            viewModel = new LoginViewModel();
        }


        public bool IsDarkTheme { get; set; }
        private readonly PaletteHelper paletteHelper = new PaletteHelper();

        private void toggleTheme(object sender, RoutedEventArgs e)
        {

            ITheme theme = paletteHelper.GetTheme();
            if (IsDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark)
            {
                IsDarkTheme = false;
                theme.SetBaseTheme(Theme.Light);

                var imageBrush = new ImageBrush(new BitmapImage(new Uri("E:\\РПМ\\УП_ПРАКТИКА\\WpfApp1\\WpfApp1\\Picture\\lightfon.jpg", UriKind.Relative)));
                this.Background = imageBrush;




            }
            else
            {
                IsDarkTheme = true;
                theme.SetBaseTheme(Theme.Dark);

                var imageBrush = new ImageBrush(new BitmapImage(new Uri("E:\\РПМ\\УП_ПРАКТИКА\\WpfApp1\\WpfApp1\\Picture\\darkfon.jpg", UriKind.Relative)));
                this.Background = imageBrush;



            }

            paletteHelper.SetTheme(theme);

        }

        private void exitApp(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = (PasswordBox)sender;
            viewModel.Password = passwordBox.Password;
            
        }
    }
}
