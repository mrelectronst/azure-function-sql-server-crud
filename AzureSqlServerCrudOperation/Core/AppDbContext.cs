using Microsoft.EntityFrameworkCore;

namespace AzureSqlServerCrudOperation.Core
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
