using System.Net;

namespace MailMan.Services.MailSenderService
{
    public class ServerConfiguration
    {


        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseSSL { get; set; }
        public NetworkCredential Credentials { get; set; }
        public ServerConfiguration()
        {
        }
        public ServerConfiguration(string host, int port, bool useSSL, NetworkCredential credentials)
        {
            Host = host;
            Port = port;
            UseSSL = useSSL;
            Credentials = credentials;
        }
    }
}