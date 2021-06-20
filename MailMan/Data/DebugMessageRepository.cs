using System;
using System.Linq;

using MailMan.Models;
using MailMan.Services.Repositories;

namespace MailMan.Data
{
    class DebugMessageRepository : DebugRepository<Message>, IMessageRepository
    {
        public DebugMessageRepository() : base(TestData.Messages) { }

        
    }
}
