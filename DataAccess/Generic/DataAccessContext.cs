using Microsoft.EntityFrameworkCore;
using ModelLibrary;

namespace DataAccess
{
    public class DataAccessContext : DbContext
    {
        public DbSet<ProductRobot> ProductRobots { get; set; }
        public DbSet<ProductSoftware> ProductSoftwares { get; set; }

        public string _path { get; set; }

        public DataAccessContext()
        {
            _path = @"D:\APPTMSQMSService\DataAccess\TMSQMS.db";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={_path}");
        }
    }
}
