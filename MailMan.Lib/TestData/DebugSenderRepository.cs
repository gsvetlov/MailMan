using System;
using System.Collections.Generic;
using System.Linq;

using MailMan.Models;
using MailMan.Services.Repositories;

namespace MailMan.Data
{
    public class DebugSenderRepository : DebugRepository<Sender>
    {
        public DebugSenderRepository() : base(TestData.Senders) { }
        public DebugSenderRepository(List<Sender> set) : base(set) { }
        
    }
}
