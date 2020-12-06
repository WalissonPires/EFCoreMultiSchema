using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppTest.Database.Entities;

namespace WebAppTest.Database
{
    public class AppDbContext : DbContext
    {
        public const string DefaultSchema = "public";
        public const string SchemaPrefix = "account_";

        public DbSet<Contact> Contacts { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var contactBuilder = modelBuilder.Entity<Contact>();
            contactBuilder.HasKey(x => x.Id);
            contactBuilder.Property(x => x.Id).UseSerialColumn().ValueGeneratedOnAdd();
            contactBuilder.Property(x => x.Name).HasMaxLength(60).IsRequired();
            contactBuilder.Property(x => x.Phone).HasMaxLength(13);
        }
    }
}
