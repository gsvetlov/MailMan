using MailMan.Models.Base;

namespace MailMan.Models
{
    public class Sender : Entity
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }
    }
}
