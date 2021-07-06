using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MailMan.Models.Base;

namespace MailMan.Models
{
    public class MailingList : Entity
    {
        public string Name { get; set; }
        public ICollection<Sender> Senders { get; set; }
        public ICollection<Server> Servers { get; set; }
        public ICollection<Recipient> Recipients { get; set; }
        public Message Message { get; set; }
    }
}
