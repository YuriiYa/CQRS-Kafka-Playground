using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Post.Query.Infrastructure.DataAccess
{
    public class DatabaseContextFactory
    {
        private readonly Action<DbContextOptionsBuilder> _configureDBContext;

        public DatabaseContextFactory(Action<DbContextOptionsBuilder> configureDBContext)
        {
            _configureDBContext = configureDBContext;
        }
        public DatabaseContext CreateContext()
        {
            DbContextOptionsBuilder<DatabaseContext> optionsBuilder = new();
            _configureDBContext(optionsBuilder);
            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}