using EFCoreMultiSchema;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppTest.Database
{
    public class AppDbContextOptionsBuilder
    {
        private readonly IConfiguration _config;

        public AppDbContextOptionsBuilder(IConfiguration configuration)
        {
            _config = configuration;
        }

        public void Configure(DbContextOptionsBuilder optionsBuilder, string schema)
        {
            var connectionString = _config.GetConnectionString("DefaultConnection");

            connectionString = new ConnectionStringBuild(connectionString)
                .SetParam(ConnectionStringParams.SEARCH_PATH, schema)
                .GetConnectionString();

            optionsBuilder.UseNpgsql(connectionString, 
                builder => builder.MigrationsHistoryTable("__EFMigrationsHistory", schema));
        }
    }
}
