using System;

using MailMan.Models;
using MailMan.Services.Repositories;

namespace MailMan.Data
{
    public class DebugSenderRepository : DebugRepository<Sender>, ISenderRepository
    {
        public DebugSenderRepository() : base(TestData.Senders) { }

        public override Sender Create(params object[] parameters) => throw new NotImplementedException();
    }

}
