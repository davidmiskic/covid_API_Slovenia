using CovidAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

public class DataGenerator
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new CaseContext(
            serviceProvider.GetRequiredService<DbContextOptions<CaseContext>>()))
        {
            if (context.Cases.Any())
            {
                return;   // Data was already seeded
            }
            // debug: Directory.SetCurrentDirectory(@"C:....");
            var client = new WebClient();
            client.DownloadFile("https://raw.githubusercontent.com/sledilnik/data/master/csv/region-cases.csv", "db.csv");
            
            StreamReader file = new StreamReader("db.csv");
            string line;
            int count = 0;
            List<Case> CSVCases = new List<Case>();

            while ((line = file.ReadLine()) != null)
            {
                if (count == 0) continue;
                count++;

                string[] fields = line.Split(',');
                CSVCases.Add(
                new Case
                {
                    Id = fields[0],
                    Region = fields[1]
                });

                if (count > 10) break;

                //decimal amount = decimal.Parse(fields[5].ToString()) * decimal.Parse("0.01");


                //cmd.Parameters.AddWithValue("@Payor", Security.RemoveSpecialCharacters(fields[1].ToString()).Trim());
                //cmd.Parameters.AddWithValue("@Amount", amount); Security.RemoveSpecialCharacters(fields[8].ToString()).Trim());
                //cmd.Parameters.AddWithValue("@Account", Security.RemoveSpecialCharacters(fields[2].ToString()));
                //cmd.Parameters.AddWithValue("@Serial", Security.RemoveSpecialCharacters(fields[0].ToString()).Trim().Trim());

            }

            context.Cases.AddRange(CSVCases);


            //context.Cases.AddRange(
            //    new Case
            //    {
            //        Id = "Test Post 1caseseed",
            //        Region = "This is my standard post for testing"
            //    },
            //    new Case
            //    {
            //        Id = "Test Post 2caseseed",
            //        Region = "This is my standard post for testing"
            //    });

            context.SaveChanges();
        }
    }
}