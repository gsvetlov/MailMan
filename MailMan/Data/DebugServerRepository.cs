using System;

using MailMan.Models;
using MailMan.Services.Repositories;

namespace MailMan.Data
{
    class DebugServerRepository : DebugRepository<Server>, IServerRepository
    {
        public DebugServerRepository() : base(TestData.Servers) { }

        public override Server Create(params object[] parameters) => throw new NotImplementedException();
    }
}
