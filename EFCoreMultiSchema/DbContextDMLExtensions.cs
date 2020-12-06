using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreMultiSchema
{
    public static class DbContextDMLExtensions
    {
        public static async Task<bool> SchemaExists(this DatabaseFacade databaseFacade, string schema)
        {
            var result = await databaseFacade.ExecuteQuery<SqlResultInt>($"SELECT 1 FROM information_schema.schemata WHERE schema_name = '{schema}'");
            return result.Count > 0;
        }

        public static async Task<bool> DatabaseExists(this DatabaseFacade databaseFacade)
        {
            var rdc = databaseFacade.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
            return await rdc.ExistsAsync();
        }

        public static async Task CreateDatabase(this DatabaseFacade databaseFacade)
        {
            var rdc = databaseFacade.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
            await rdc.CreateAsync();
        }

        public static async Task CreateSchema(this DatabaseFacade databaseFacade, string schema)
        {
            await databaseFacade.ExecuteSqlRawAsync("CREATE SCHEMA " + schema);
        }

    }
}
