﻿using MailMan.Models.Base;

namespace MailMan.Models
{
    public class Server : BaseModel
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public int Port { get; set; } = 25;

        public bool UseSSL { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Description { get; set; }
    }
}
