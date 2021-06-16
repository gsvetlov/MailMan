using System.Collections.Generic;

using MailMan.Services.MailSenderService.Enums;

namespace MailMan.Services.MailSenderService
{
    public class MessageConfiguration
    {
        public MessagePriority Priority { get; set; }
        public string From { get; set; }
        public List<string> To { get; set; }
        public List<string> CC { get; set; }
        public List<string> ReplyTo { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsBodyHtml { get; set; }

    }
}