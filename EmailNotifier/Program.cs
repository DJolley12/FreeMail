using FreeMail.Service;
using System;
using System.Text;
using System.Xml;

namespace FreeMailConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var gmailService = new GmailService("dcjolley12@gmail.com", "7Strings");
            var message = gmailService.GetMostRecentMessage();
            Console.WriteLine(message.MessagePart.Body);
        }
    }
}
