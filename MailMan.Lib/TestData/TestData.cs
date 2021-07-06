using System.Collections.Generic;
using System.Linq;

using MailMan.Models;

namespace MailMan.Data
{
    public class TestData
    {
        public static List<Server> Servers { get; } = Enumerable.Range(1, 10)
           .Select(i => new Server
           {
               Id = i,
               Name = $"Сервер {i}",
               Address = $"smtp.server-{i}.ru",
               Login = $"User-{i}",
               Password = $"Password - {i}",
               UseSSL = i % 2 == 0,
               Description = $"Описание сервера {i}"

           })
           .ToList();

        public static List<Sender> Senders { get; } = Enumerable.Range(1, 15)
           .Select(i => new Sender
           {
               Id = i,
               Name = $"Отправитель - {i}",
               Address = $"sender-{i}@server.ru",
               Description = $"Описание отправителя {i}",
           })
           .ToList();

        public static List<Recipient> Recipients { get; } = Enumerable.Range(1, 30)
           .Select(i => new Recipient
           {
               Id = i,
               Name = $"Получатель - {i}",
               Address = $"recipient-{i}@server.ru",
               Description = $"Описание получателя {i}"
           })
           .ToList();

        public static List<Message> Messages { get; } = Enumerable.Range(1, 100)
           .Select(i => new Message
           {
               Id = i,
               Title = $"Сообщение {i}",
               Text = $"Текст сообщения {i}"
           })
           .ToList();

        public static List<MailingList> MailingLists { get; } = Enumerable.Range(1, 10)
           .Select(i => new MailingList
           {
               Id = i,
               Name = $"Список рассылки {i}",
               Message = Messages[i * 3],
               Senders = new List<Sender>() { Senders[0], Senders[Senders.Count - i] },
               Servers = new List<Server> { Servers[i - 1] },
               Recipients = new List<Recipient> { Recipients[i], Recipients[i * 2 - 1], Recipients[i * 3 - 2] }
           })
           .ToList();
    }
}
