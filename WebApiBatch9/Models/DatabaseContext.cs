using Microsoft.EntityFrameworkCore;

namespace WebApiBatch9.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() { }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)

        {

        }

        public virtual DbSet<CurdModel> curdmodel { get; set; }
    }
}
