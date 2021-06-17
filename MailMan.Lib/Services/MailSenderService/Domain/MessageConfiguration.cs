using System;
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
        public MessageConfiguration()
        {
        }
        public MessageConfiguration(MessagePriority priority, string from, List<string> to, List<string> cC, List<string> replyTo, string subject, string body, bool isBodyHtml)
        {
            Priority = priority;
            From = from ?? throw new ArgumentNullException(nameof(from));
            To = to ?? throw new ArgumentNullException(nameof(to));
            CC = cC ?? throw new ArgumentNullException(nameof(cC));
            ReplyTo = replyTo ?? throw new ArgumentNullException(nameof(replyTo));
            Subject = subject ?? throw new ArgumentNullException(nameof(subject));
            Body = body ?? throw new ArgumentNullException(nameof(body));
            IsBodyHtml = isBodyHtml;
        }
    }
}