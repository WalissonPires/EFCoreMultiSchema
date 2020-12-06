using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppTest.Database
{
    public class AppDbContextDesignFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {            
           var options = new DbContextOptionsBuilder()
                .UseNpgsql("Host=localhost;Port=5432;Database=DbForMigration;User ID=postgres;Password=masterkey;")
                .Options;

            return new AppDbContext(options);
        }
    }
}
