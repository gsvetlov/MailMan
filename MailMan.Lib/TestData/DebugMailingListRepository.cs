using System;
using System.Collections.Generic;
using System.Linq;

using MailMan.Models;
using MailMan.Services.Repositories;

namespace MailMan.Data
{
    public class DebugMailingListRepository : DebugRepository<MailingList>
    {
        public DebugMailingListRepository() : base(TestData.MailingLists) { }
        public DebugMailingListRepository(List<MailingList> set) : base(set) { }
    }
}
