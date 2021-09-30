using CovidAPI.Models;

public class CaseSeeder
{
    private readonly CaseContext _context;
    public CaseSeeder(CaseContext context)
    {
        _context = context;
    }

    public void SeedData()
    {
        _context.Cases.Add(new Case
        {
            Id = "Test Post 1caseseeder",
            Region = "This is my standard post for testing"
        });
        _context.Cases.Add(new Case
        {
            Id = "Test Post 2caseseeder",
            Region = "This is my aside post for testing"
        });

        _context.SaveChanges();
    }
}