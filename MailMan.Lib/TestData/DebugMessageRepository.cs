using System;
using System.Collections.Generic;
using System.Linq;

using MailMan.Models;
using MailMan.Services.Repositories;

namespace MailMan.Data
{
    public class DebugMessageRepository : DebugRepository<Message>
    {
        public DebugMessageRepository() : base(TestData.Messages) { }
        public DebugMessageRepository(List<Message> set) : base(set) { }


    }
}
