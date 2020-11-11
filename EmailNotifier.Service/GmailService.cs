using System;
using OpenPop;
using OpenPop.Pop3;
using OpenPop.Mime;
using System.Text;
using System.Collections.Generic;
using FreeMail.Domain;

namespace FreeMail.Service
{
    public class GmailService
    {
        private Pop3Client client { get; set; }
        private DateTime LastLoggedIn { get; set; }
        private string _email { get; set; }
        private string _password { get; set; }

        public GmailService(string email, string password)
        {
            _email = email;
            _password = password;
            Authenticate();
        }

        private void Authenticate()
        {
            var previousHour = DateTime.Now.Hour - 2;
            if (LastLoggedIn == null || LastLoggedIn.Day != DateTime.Now.Day && LastLoggedIn.Hour <= previousHour)
            {
                client = new Pop3Client();
                client.Connect("pop.gmail.com", 995, true);
                client.Authenticate($"recent:{_email}", _password);
                LastLoggedIn = DateTime.Now;
            }
        }

        public List<Email> GetInboxMessages()
        {
            Authenticate();
            var count = client.GetMessageCount();
            var emails = new List<Email>();
            for (int i = 1; i < count; i++)
            {
                var message = client.GetMessage(i);
                var email = new Email(message);
                emails.Add(email);
            }
            return emails;
        }

        public Message GetMostRecentMessage()
        {
            Authenticate();
            var count = client.GetMessageCount();
            Message message = client.GetMessage(count);
            return message;
        }

        public string GetMessageBody(Message message)
        {
            Authenticate();
            MessagePart body = message.FindFirstPlainTextVersion();
            return body.GetBodyAsText();
        }

        public string GetMessageSubject(Message message)
        {
            Authenticate();
            var plainText = message.Headers;
            return plainText.Subject;
        }
    }
}
