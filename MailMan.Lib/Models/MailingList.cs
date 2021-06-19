using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailMan.Models
{
    public class MailingList
    {
        public string Name { get; set; }
        public List<Sender> Senders { get; set; }
        public List<Server> Servers { get; set; }
        public List<Recipient> Recipients { get; set; }
        public Message Message { get; set; }
    }
}
