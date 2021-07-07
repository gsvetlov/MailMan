
using MailMan.Models;
using MailMan.Services.Repositories.Base;
using MailMan.Services.Repositories.Db;

namespace MailMan.Services.Repositories
{
    public class ServerRepository : BaseRepositoryAsync<Server>
    {
        public ServerRepository(MailManDB db) : base(db) { }

    }
}
