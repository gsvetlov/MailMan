using System;

using MailMan.Models;
using MailMan.Services.Repositories;

namespace MailMan.Data
{
    class DebugMailingListRepository : DebugRepository<MailingList>, IMailingListRepository
    {
        public override MailingList Create(params object[] parameters) => throw new NotImplementedException();
    }
}
