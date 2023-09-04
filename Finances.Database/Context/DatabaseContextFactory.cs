using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Finances.Database.Context
{
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            var connString = "server=localhost;database=finances;user=root;password=!root";

            if (args != null && args.Length > 0)
            {
                connString = args[0];
            }

            optionsBuilder.UseMySql(connString, ServerVersion.AutoDetect(connString));

            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}
