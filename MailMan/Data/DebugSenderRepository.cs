using System;
using System.Linq;

using MailMan.Models;
using MailMan.Services.Repositories;

namespace MailMan.Data
{
    public class DebugSenderRepository : DebugRepository<Sender>, ISenderRepository
    {
        public DebugSenderRepository() : base(TestData.Senders) { }

        
    }
}
