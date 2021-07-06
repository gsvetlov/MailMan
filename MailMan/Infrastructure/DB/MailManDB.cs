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

        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);

            model.Entity<Message>()
                .Property(p => p.Title)
                .HasMaxLength(128);
            model.Entity<Message>()
                .Property(p => p.Text)
                .HasMaxLength(1024);

            model.Entity<Server>()
                .Property(p => p.Address)
                .HasMaxLength(64);
            model.Entity<Server>()
                .Property(p => p.Name)
                .HasMaxLength(64);
            model.Entity<Server>()
                .Property(p => p.Login)
                .HasMaxLength(64);
            model.Entity<Server>()
                .Property(p => p.Password)
                .HasMaxLength(64);
            model.Entity<Server>()
                .Property(p => p.Description)
                .HasMaxLength(128);

            model.Entity<Sender>()
                .Property(p => p.Name)
                .HasMaxLength(64);
            model.Entity<Sender>()
                .Property(p => p.Address)
                .HasMaxLength(64);
            model.Entity<Sender>()
                .Property(p => p.Description)
                .HasMaxLength(128);

            model.Entity<Recipient>()
                .Property(p => p.Name)
                .HasMaxLength(64);
            model.Entity<Recipient>()
                .Property(p => p.Address)
                .HasMaxLength(64);
            model.Entity<Recipient>()
                .Property(p => p.Description)
                .HasMaxLength(128);

            model.Entity<MailingList>()
                .Property(p => p.Name)
                .HasMaxLength(64);

        }
    }
}
