using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace WpfApp1.Views
{
    /// <summary>
    /// Логика взаимодействия для SettingPage.xaml
    /// </summary>
    public partial class SettingPage : Page
    {
        
        public bool IsDarkTheme { get; set; }
        private readonly PaletteHelper paletteHelper = new PaletteHelper();
        public SettingPage()
        {
            InitializeComponent();
        }
        
        private void SwitchToggle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            //ITheme theme = paletteHelper.GetTheme();
            //if (IsDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark)
            //{
            //    IsDarkTheme = false;
            //    theme.SetBaseTheme(Theme.Light);

            //    var imageBrush = new ImageBrush(new BitmapImage(new Uri("E:\\РПМ\\УП_ПРАКТИКА\\WpfApp1\\WpfApp1\\Picture\\lightfon.jpg", UriKind.Relative)));

            //    mainWindow.Background = imageBrush;




            //}
            //else
            //{
            //    IsDarkTheme = true;
            //    theme.SetBaseTheme(Theme.Dark);

            //    var imageBrush = new ImageBrush(new BitmapImage(new Uri("E:\\РПМ\\УП_ПРАКТИКА\\WpfApp1\\WpfApp1\\Picture\\darkfon.jpg", UriKind.Relative)));

            //    mainWindow.Background = imageBrush;
            //}

            //paletteHelper.SetTheme(theme);



            //var uri = new Uri($"/Styles/Dark.xaml", UriKind.Relative);
            //ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            //Application.Current.Resources.Clear();
            //Application.Current.Resources.MergedDictionaries.Add(resourceDict);


            //var uri = new Uri($"/Styles/Ligt.xaml", UriKind.Relative);
            //ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            //Application.Current.Resources.Clear();
            //Application.Current.Resources.MergedDictionaries.Add(resourceDict);
            Main main = Application.Current.Windows.OfType<Main>().FirstOrDefault();
            if (BtnToggle.Toggled1 == true)
            {
                ResourceDictionary dark = new ResourceDictionary();
                dark.Source = new Uri("E:\\РПМ\\УП_ПРАКТИКА\\WpfApp1\\WpfApp1\\Styles\\Dark.xaml", UriKind.Absolute);
                System.Windows.Application.Current.Resources.MergedDictionaries.Add(dark);
            }
            else
            {
                ResourceDictionary Light = new ResourceDictionary();
                Light.Source = new Uri("E:\\РПМ\\УП_ПРАКТИКА\\WpfApp1\\WpfApp1\\Styles\\Light.xaml", UriKind.Absolute);
                System.Windows.Application.Current.Resources.MergedDictionaries.Add(Light);
            }
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }
}
