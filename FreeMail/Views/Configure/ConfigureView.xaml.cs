using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FreeMail.Views.ConfigureView
{
    /// <summary>
    /// Interaction logic for ConfigureView.xaml
    /// </summary>
    public partial class ConfigureView : UserControl
    {
        public ConfigureView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            Settings.Default.EmailAddress = EmailAddressTextBox.Text.Trim().ToLower();
            Settings.Default.Password = PasswordTextBox.Text.Trim();
            Settings.Default.Configured = true;
            Settings.Default.Save();
        }
    }
}
