using System;
using System.Diagnostics;

using MailMan.Services.MailSenderService;

namespace MailMan.Data
{
    public class DebugMailService : IMailSenderService
    {
        public void SendMail(MessageConfiguration message, ServerConfiguration server)
        {
            Debug.WriteLine("sending mail");
            SendMailResultEvent?.Invoke(this, new(true, "The mail has been sent"));
        }

        public event EventHandler<SendMailResult> SendMailResultEvent;
    }
}
