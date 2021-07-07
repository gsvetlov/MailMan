
using MailMan.Models;
using MailMan.Services.Repositories.Base;
using MailMan.Services.Repositories.Db;

namespace MailMan.Services.Repositories
{
    public class MessageRepository : BaseRepositoryAsync<Message>
    {
        public MessageRepository(MailManDB db) : base(db) { }

    }
}
