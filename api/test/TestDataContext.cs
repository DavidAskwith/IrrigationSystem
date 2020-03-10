using Irrigation.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;

namespace Test
{
    public class TestDataContext : DataContext
    {
        public TestDataContext()
            : base(InitializeOptions())
        {
        }

        public static DbContextOptions<DataContext> InitializeOptions()
        {
           // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<DataContext>()
                .UseSqlite(connection)
                .Options; 

            using(var ctx = new DataContext(options))
            ctx.Database.EnsureCreated();

            return options;
        }
    }
}
