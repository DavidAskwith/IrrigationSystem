using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace IrigationSystem
{
    public class IrigationSystemContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=blogging.db");
    }
}
