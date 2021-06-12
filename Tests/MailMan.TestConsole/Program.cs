using System;
using System.Net;
using System.Net.Mail;

using Microsoft.Extensions.Configuration;

namespace MailMan.TestConsole
{
    class Program
    {
        public static IConfiguration Configuration { get; set; }

        static void Main(string[] args)
        {
            Init();
            SendMail();
        }

        private static void Init()
        {
            var builder = new ConfigurationBuilder();
            builder.AddUserSecrets<Program>();
            Configuration = builder.Build();
        }
        private static void SendMail()
        {
            // готовим сообщение
            using MailMessage msg = new("svetlov.georg@yandex.ru", "gsvetlov@gmail.com");
            msg.Subject = "Тестовое сообщение от " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ff");
            msg.Body = "Привет от самого себя\nТело тестового сообщения " + DateTime.Now.ToString("F");

            // готовим smtp-клиент
            using SmtpClient client = new("smtp.yandex.ru", 25)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential()
                {
                    UserName = "svetlov.georg",
                    // берем пароль из user-secrets
                    Password = Configuration["smtp-token"] ?? throw new ArgumentNullException("token is missing in user-secrets")
                }
            };
            try
            {
                Console.Write("sending mail: ");
                client.Send(msg);
                Console.WriteLine("successfull");
            }
            catch (SmtpException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
