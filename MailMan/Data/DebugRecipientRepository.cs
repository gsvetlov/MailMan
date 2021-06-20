using System.Linq;

using MailMan.Models;
using MailMan.Services.Repositories;

namespace MailMan.Data
{
    public class DebugRecipientRepository : DebugRepository<Recipient>, IRecipientRepository
    {
        public DebugRecipientRepository() : base(TestData.Recipients) { }

       
    }
}
