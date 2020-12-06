using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreMultiSchema
{
    public class DbContextMigration
    {
        public async Task ExecuteMigration(DbContext _db, string schema)
        {            
            if (schema != null)
            {
                if (!await _db.Database.DatabaseExists())
                    await _db.Database.CreateDatabase();

                if (!await _db.Database.SchemaExists(schema))
                    await _db.Database.CreateSchema(schema);
            }

            await _db.Database.MigrateAsync();
        }
    }
}
