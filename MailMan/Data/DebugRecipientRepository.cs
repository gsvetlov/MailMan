using System;

using MailMan.Models;
using MailMan.Services.Repositories;

namespace MailMan.Data
{
    public class DebugRecipientRepository : DebugRepository<Recipient>, IRecipientRepository
    {
        public DebugRecipientRepository() : base(TestData.Recipients) { }

        public override Recipient Create(params object[] parameters) => throw new NotImplementedException();
    }
}
