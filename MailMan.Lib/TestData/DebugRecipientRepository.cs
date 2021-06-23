using System.Collections.Generic;
using System.Linq;

using MailMan.Models;
using MailMan.Services.Repositories;

namespace MailMan.Data
{
    public class DebugRecipientRepository : DebugRepository<Recipient>
    {
        public DebugRecipientRepository() : base(TestData.Recipients) { }
        public DebugRecipientRepository(List<Recipient> set) : base(set) { }


    }
}
