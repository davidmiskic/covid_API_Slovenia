using CovidAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

public class DataGenerator
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new CaseContext(
            serviceProvider.GetRequiredService<DbContextOptions<CaseContext>>()))
        {
            // Look for any board games.
            if (context.Cases.Any())
            {
                return;   // Data was already seeded
            }

            context.Cases.AddRange(
                new Case
                {
                    Id = "Test Post 1caseseed",
                    Region = "This is my standard post for testing"
                },
                new Case
                {
                    Id = "Test Post 2caseseed",
                    Region = "This is my standard post for testing"
                });

            context.SaveChanges();
        }
    }
}