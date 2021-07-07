
using MailMan.Models;
using MailMan.Services.Repositories.Base;
using MailMan.Services.Repositories.Db;

namespace MailMan.Services.Repositories
{
    public class SenderRepository : BaseRepositoryAsync<Sender>
    {
        public SenderRepository(MailManDB db) : base(db) { }

    }
}
