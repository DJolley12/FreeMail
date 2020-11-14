using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using FreeMail.Domain;
using FreeMail.Service;
using FreeMail.Properties;

namespace FreeMail
{
    public class EmailChecker
    {
        private readonly GmailService _gmailService;
        NotifyIcon notifyIcon { get; set; }
        public EmailChecker(GmailService gmailService)
        {
            _gmailService = gmailService;
        }
        public async Task CheckForNewMailAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var email = _gmailService.GetMostRecentEmail();
                if (email.DateSentLocal != Settings.Default.SentTime && 
                    email.Sender != Settings.Default.Sender || 
                    Settings.Default.SentTime == null ||
                    Settings.Default.Sender == null
                    )
                {
                    Settings.Default.SentTime = email.DateSentLocal;
                    Settings.Default.Sender = email.Sender;
                    Settings.Default.Save();
                    NewMessageNotification(email);
                }

                await Task.Delay(/*60 * 5000*/10000);
            }
        }

        private void NewMessageNotification(Email email)
        {
            notifyIcon = new NotifyIcon()
            {
                Visible = true,
                Icon = System.Drawing.SystemIcons.Information,
                BalloonTipTitle = email.Sender,
                BalloonTipText = email.Subject
            };

            notifyIcon.ShowBalloonTip(10000);

            Thread.Sleep(10000);

            notifyIcon.Dispose();
        }
    }
}
