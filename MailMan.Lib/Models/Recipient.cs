﻿using MailMan.Models.Base;

namespace MailMan.Models
{
    public class Recipient : BaseModel
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }
    }
}
