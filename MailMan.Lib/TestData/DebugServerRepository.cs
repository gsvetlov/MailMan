using System.Collections.Generic;
using System.Linq;

using MailMan.Models;
using MailMan.Services.Repositories;

namespace MailMan.Data
{
    public class DebugServerRepository : DebugRepository<Server>
    {
        public DebugServerRepository() : base(TestData.Servers) { }
        public DebugServerRepository(List<Server> set) : base(set) { }


    }
}
