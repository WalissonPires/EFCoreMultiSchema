using EFCoreMultiSchema;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppTest.Database
{
    public class AppDbContextFactory
    {        
        private readonly AppDbContextOptionsBuilder _dbOoptionsBuilder;

        public AppDbContextFactory(AppDbContextOptionsBuilder dbOoptionsBuilder)
        {            
            _dbOoptionsBuilder = dbOoptionsBuilder;
        }

        public AppDbContext Create(string schema)
        {
            var optionsBuider = new DbContextOptionsBuilder<AppDbContext>();
            _dbOoptionsBuilder.Configure(optionsBuider, schema);

            var dbContext = new AppDbContext(optionsBuider.Options);
            return dbContext;
        }
    }
}
