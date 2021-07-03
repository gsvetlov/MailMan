using MailMan.Models.Base;

namespace MailMan.Models
{
    public class Message : Entity
    {
        public string Title { get; set; }

        public string Text { get; set; }
    }
}
