using System;
using System.ComponentModel;
using System.Net.Mail;

using MailMan.Services.MailSenderService.Enums;

namespace MailMan.Services.MailSenderService
{
    public class SimpleSmtpService : IMailSenderService
    {
        #region singleton implementation
        private static readonly object _lock = new();
        private static volatile IMailSenderService _instance;
        public static IMailSenderService GetService()
        {
            if (_instance == null)
                lock (_lock)
                    if (_instance is null)
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
                EnableSsl = server.UseSSL,
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
                SendMailResultEvent?.Invoke(this, new SendMailResult(false, e.Message));
            }
        }

        private void OnClientSendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            var isSuccessfull = !e.Cancelled && e.Error == null;
            var message = e?.Error?.Message ?? "Operation completed";
            var result = new SendMailResult
            {
                IsSuccessfull = isSuccessfull,
                Message = message
            };
            SendMailResultEvent?.Invoke(this, result);
        }
    }
}
