using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailMan.Services.MailSenderService
{
    public interface IMailSenderService
    {
        void SendMail(MessageConfiguration message, ServerConfiguration server);
        event EventHandler<SendMailResult> SendMailResultEvent;
    }
}
