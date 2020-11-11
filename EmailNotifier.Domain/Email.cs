using OpenPop.Mime;
using System;
using System.Collections.Generic;

namespace FreeMail.Domain
{
    public class Email
    {
        public string Subject { get; set; }
        public string Sender { get; set; }
        public List<string> CCAddresses { get; set; }
        public string Body { get; set; }
        public DateTime DateSentLocal { get; set; }

        public Email(Message message)
        {
            Subject = message.Headers.Subject;
            Sender = message.Headers.From.ToString();
            CCAddresses = new List<string>();
            foreach (var cc in message.Headers.Cc)
            {
                CCAddresses.Add(cc.ToString());
            }
            var textBody = message.FindFirstPlainTextVersion();
            var htmlBody = message.FindFirstHtmlVersion();
            if (textBody != null)
            {
                Body = textBody.GetBodyAsText();
            }
            else if (htmlBody != null)
            {
                Body = htmlBody.GetBodyAsText();
            }
            DateSentLocal = message.Headers.DateSent.ToLocalTime();
        }
    }
}
