using System;

using MailMan.Models;
using MailMan.Services.Repositories;

namespace MailMan.Data
{
    class DebugMessageRepository : DebugRepository<Message>, IMessageRepository
    {
        public DebugMessageRepository() : base(TestData.Messages) { }

        public override Message Create(params object[] parameters) => throw new NotImplementedException();
    }
}
