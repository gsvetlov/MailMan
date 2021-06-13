using System;
using System.Net.Mail;

using MailMan.Services.MailSenderService.Enums;

namespace MailMan.Services.MailSenderService.impl
{
    class SimpleSmtpService : IMailSenderService
    {
        #region singleton implementation
        private static object _lock = new();
        private static IMailSenderService _instance;
        public static IMailSenderService GetService()
        {
            if (_instance == null)
                lock (_lock)
                    _instance = new SimpleSmtpService();
            return _instance;
        }
        #endregion

        private SimpleSmtpService() { }

        public event EventHandler<SendMailResult> SendMailResultEvent;

        public void SendMail(MessageConfiguration message, ServerConfiguration server) => Send(message, server);


        private void Send(MessageConfiguration message, ServerConfiguration server)
        {
            using MailMessage msg = new()
            {
                From = new MailAddress(message.From),
                Subject = message.Subject,
                Body = message.Body,
                IsBodyHtml = message.IsBodyHtml
            };
            msg.Sender = msg.From;
            message.To.ForEach(entry => msg.To.Add(entry));
            message.CC?.ForEach(entry => msg.CC.Add(entry));
            message.ReplyTo?.ForEach(entry => msg.ReplyToList.Add(entry));
            msg.Priority = message.Priority switch
            {
                MessagePriority.Urgent or MessagePriority.High => MailPriority.High,
                MessagePriority.Medium => MailPriority.Normal,
                _ => MailPriority.Low
            };

            using SmtpClient client = new()
            {
                Host = server.Host,
                Port = server.Port,
                EnableSsl = true,
                Credentials = server.Credentials
            };
            client.SendCompleted += OnClientSendCompleted;

            SendAsync(msg, client);

        }

        private void SendAsync(MailMessage msg, SmtpClient client)
        {
            try
            {
                client.SendAsync(msg, null);
            }
            catch (Exception e)
            {
                SendMailResultEvent?.Invoke(this, new(false, e.Message));
            }
        }

        private void OnClientSendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            SendMailResult result = new()
            {
                IsSuccessfull = !e.Cancelled && e.Error == null,
                Message = e?.Error.Message ?? "Operation completed"
            };
            SendMailResultEvent?.Invoke(this, result);
        }
    }
}
