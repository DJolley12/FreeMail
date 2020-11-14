using FreeMail.Domain;
using FreeMail.Service;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace FreeMail.Views
{
    /// <summary>
    /// Interaction logic for InboxView.xaml
    /// </summary>
    public partial class InboxView : UserControl
    {
        private readonly GmailService _gmailService;
        public List<string> EmailSubjects { get; set; }
        public List<Email> Emails { get; set; }
        public InboxView(GmailService gmailService)
        {
            InitializeComponent();
            _gmailService = gmailService;
            var emails = _gmailService.GetInboxMessages();
            Emails = emails;
            EmailSubjects = Emails.Select(e => e.Subject).ToList();
        }
    }
}
