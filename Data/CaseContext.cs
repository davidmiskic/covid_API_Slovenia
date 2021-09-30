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

        public void FillFromCSV()
        {
            Cases.Add(new Case
            {
                Id = "Test Post 1",
                Region = "This is my standard post for testing"
            });
            Cases.Add(new Case
            {
                Id = "Test Post 2",
                Region = "This is my aside post for testing"
            });
        }
    }
}
