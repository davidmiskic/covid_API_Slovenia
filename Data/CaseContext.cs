using Microsoft.EntityFrameworkCore;

namespace CovidAPI.Models
{
    public class CaseContext : DbContext
    {
        public CaseContext(DbContextOptions<CaseContext> options)
            : base(options)
        {
        }

        public DbSet<Case> Cases { get; set; }
    }
}
