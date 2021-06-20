using System;
using System.Diagnostics;

using MailMan.Services.MailSenderService;

namespace MailMan.Data
{
    class DebugMailService : IMailSenderService
    {
        public void SendMail(MessageConfiguration message, ServerConfiguration server) => Debug.WriteLine("sending mail");

        public event EventHandler<SendMailResult> SendMailResultEvent;
    }
}
