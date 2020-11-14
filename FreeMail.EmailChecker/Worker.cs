using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FreeMail.Domain;
using FreeMail.Service;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Tulpep.NotificationWindow;

namespace EmailChecker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly GmailService _gmailService;

        public Worker(ILogger<Worker> logger, GmailService gmailService)
        {
            _logger = logger;
            _gmailService = gmailService;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var email = _gmailService.GetInboxMessages().First();
                NewMessageNotification(email);

                await Task.Delay(/*60 * 5000*/10000);
            }
        }

        private void NewMessageNotification(Email email)
        {
            PopupNotifier popup = new PopupNotifier();
            popup.TitleText = $"New email from {email.Sender}";
            popup.ContentText = $"{email.Subject}";
            popup.Popup();

            Thread.Sleep(10000);

            popup.Dispose();
        }
    }
}
