using System;
using System.Collections.Generic;
using System.Linq;

using MailMan.Models;
using MailMan.Services.Repositories;

namespace MailMan.Data
{
    class DebugMailingListRepository : DebugRepository<MailingList>, IMailingListRepository
    {
        public DebugMailingListRepository() : base() { }
    }
}
