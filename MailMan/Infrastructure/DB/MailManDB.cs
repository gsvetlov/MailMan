using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MailMan.Models;

using Microsoft.EntityFrameworkCore;

namespace MailMan.Infrastructure.DB
{
    class MailManDB : DbContext
    {
        public DbSet<Server> Servers { get; set; }
        public DbSet<Sender> Senders { get; set; }
        public DbSet<Recipient> Recipients { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MailingList> MailingLists { get; set; }
        public MailManDB(DbContextOptions<MailManDB> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
