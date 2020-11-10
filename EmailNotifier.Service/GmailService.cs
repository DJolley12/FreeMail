using System;
using OpenPop;
using OpenPop.Pop3;
using OpenPop.Mime;
using System.Text;

namespace FreeMail.Service
{
    public class GmailService
    {
        private Pop3Client client { get; set; }

        public GmailService(string email, string password)
        {
            client = new Pop3Client();
            client.Connect("pop.gmail.com", 995, true);
            client.Authenticate($"recent:{email}", password);
        }

        public Message GetMostRecentMessage()
        {
            var count = client.GetMessageCount();
            Message message = client.GetMessage(count);
            return message;
        }

        public string GetMessageBody(Message message)
        {   
            MessagePart body = message.FindFirstPlainTextVersion();
            return body.GetBodyAsText();
        }

        public string GetMessageSubject(Message message)
        {
            var plainText = message.Headers;
            return plainText.Subject;
        }
    }
}
