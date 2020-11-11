using System;
using System.Configuration;
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
using FreeMail.Views.ConfigureView;
using FreeMail.Views;
using FreeMail.Service;

namespace FreeMail
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private  GmailService _gmailService { get; set; }
        private int resizeRatio { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            CheckConfigStatus();
            resizeRatio = 5;
        }

        private void CheckConfigStatus()
        {
            if (!Settings.Default.Configured)
            {
                DataContext = new ConfigureView();
            }
            else
            {
                _gmailService = new GmailService(Settings.Default.EmailAddress, Settings.Default.Password);
            }
            
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new ConfigureView();
        }

        private void Menu_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            App.Current.MainWindow.DragMove();
        }

        private void Menu_MouseDown(object sender, MouseButtonEventArgs e)
        {
            App.Current.MainWindow.DragMove();
        }

        private void ResizeClick(object sender, RoutedEventArgs e)
        {
            resizeRatio -= 1;
            if (resizeRatio == 1)
            {
                resizeRatio = 5;
            }
            var screenWidth = (SystemParameters.PrimaryScreenWidth / 5) * (resizeRatio - .5);
            var screenHeight = (SystemParameters.PrimaryScreenHeight / 5) * (resizeRatio - .5);
            Width = screenWidth;
            Height = screenHeight;
        }

        private void MaximizeToggleClick(object sender, RoutedEventArgs e)
        {
            if (App.Current.MainWindow.WindowState == WindowState.Maximized)
            {
                App.Current.MainWindow.WindowState = WindowState.Normal;
            }
            else if (App.Current.MainWindow.WindowState == WindowState.Normal)
            {
                App.Current.MainWindow.WindowState = WindowState.Maximized;
            }
        }

        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void InboxClick(object sender, RoutedEventArgs e)
        {
            DataContext = new InboxView(_gmailService);
        }
    }
}
