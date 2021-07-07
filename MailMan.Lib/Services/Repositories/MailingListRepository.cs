
using MailMan.Models;
using MailMan.Services.Repositories.Base;
using MailMan.Services.Repositories.Db;

namespace MailMan.Services.Repositories
{
    public class MailingListRepository : BaseRepositoryAsync<MailingList>
    {
        public MailingListRepository(MailManDB db) : base(db) { }

    }
}
