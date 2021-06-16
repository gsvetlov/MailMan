using System.Net;

namespace MailMan.Services.MailSenderService
{
    public class ServerConfiguration
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public NetworkCredential Credentials { get; set; }
    }
}