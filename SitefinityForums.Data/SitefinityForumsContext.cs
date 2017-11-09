using Microsoft.EntityFrameworkCore;
using SitefinityForums.Data;

namespace SitefinityForums.Data
{
    public class SitefinityForumsContext : DbContext
    {
        public SitefinityForumsContext(DbContextOptions<SitefinityForumsContext> options) : base(options)
        {
        }

        public DbSet<ForumThread> Threads { get; set; }
    }
}