
using MailMan.Models;
using MailMan.Services.Repositories.Base;
using MailMan.Services.Repositories.Db;

namespace MailMan.Services.Repositories
{
    public class RecipientRepository : BaseRepositoryAsync<Recipient>
    {
        public RecipientRepository(MailManDB db) : base(db) { }

    }
}

