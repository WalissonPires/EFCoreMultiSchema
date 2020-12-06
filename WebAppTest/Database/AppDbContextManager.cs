using EFCoreMultiSchema;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppTest.Database
{
    public class AppDbContextManager
    {
        private readonly AppDbContextFactory _dbFactory;

        public AppDbContextManager(AppDbContextFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task CreateEnviroment(string environmentId)
        {            
            var schema = environmentId;

            using var dbContext = _dbFactory.Create(environmentId);
            await new DbContextMigration().ExecuteMigration(dbContext, schema);            
        }

        public async Task<IEnumerable<string>> GetEnvironments()
        {
            using var dbContext = _dbFactory.Create(AppDbContext.DefaultSchema);
            var schemas = await dbContext.Database.ExecuteQuery<SchemaRow>($"SELECT schema_name FROM information_schema.schemata WHERE schema_name LIKE '{AppDbContext.SchemaPrefix}%'");

            return schemas.Select(x => x.schema_name).ToArray();
        }
    }

    class SchemaRow
    {
        public string schema_name { get; set; }
    }
}
