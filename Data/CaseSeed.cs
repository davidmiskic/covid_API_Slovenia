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
            int count = -1;
            List<Case> CSVCases = new List<Case>();

            while ((line = file.ReadLine()) != null)
            {
                count++;
                if (count == 0) continue;
                

                string[] fields = line.Split(',');
                CSVCases.Add(
                new Case
                {
                    Id = DateTime.Parse(fields[0]).Date,
                    region_ce_cases_active = fields[1],
                    region_ce_cases_confirmed_todate = fields[2],
                    region_ce_deceased_todate = fields[3],
                    region_ce_vaccinated_1st_todate = fields[4],
                    region_ce_vaccinated_2nd_todate = fields[5],
                    region_foreign_cases_active = fields[6],
                    region_foreign_cases_confirmed_todate = fields[7],
                    region_foreign_deceased_todate = fields[8],
                    region_kk_cases_active = fields[9],
                    region_kk_cases_confirmed_todate = fields[10],
                    region_kk_deceased_todate = fields[11],
                    region_kk_vaccinated_1st_todate = fields[12],
                    region_kk_vaccinated_2nd_todate = fields[13],
                    region_kp_cases_active = fields[14],
                    region_kp_cases_confirmed_todate = fields[15],
                    region_kp_deceased_todate = fields[16],
                    region_kp_vaccinated_1st_todate = fields[17],
                    region_kp_vaccinated_2nd_todate = fields[18],
                    region_kr_cases_active = fields[19],
                    region_kr_cases_confirmed_todate = fields[20],
                    region_kr_deceased_todate = fields[21],
                    region_kr_vaccinated_1st_todate = fields[22],
                    region_kr_vaccinated_2nd_todate = fields[23],
                    region_lj_cases_active = fields[24],
                    region_lj_cases_confirmed_todate = fields[25],
                    region_lj_deceased_todate = fields[26],
                    region_lj_vaccinated_1st_todate = fields[27],
                    region_lj_vaccinated_2nd_todate = fields[28],
                    region_mb_cases_active = fields[29],
                    region_mb_cases_confirmed_todate = fields[30],
                    region_mb_deceased_todate = fields[31],
                    region_mb_vaccinated_1st_todate = fields[32],
                    region_mb_vaccinated_2nd_todate = fields[33],
                    region_ms_cases_active = fields[34],
                    region_ms_cases_confirmed_todate = fields[35],
                    region_ms_deceased_todate = fields[36],
                    region_ms_vaccinated_1st_todate = fields[37],
                    region_ms_vaccinated_2nd_todate = fields[38],
                    region_ng_cases_active = fields[39],
                    region_ng_cases_confirmed_todate = fields[40],
                    region_ng_deceased_todate = fields[41],
                    region_ng_vaccinated_1st_todate = fields[42],
                    region_ng_vaccinated_2nd_todate = fields[43],
                    region_nm_cases_active = fields[44],
                    region_nm_cases_confirmed_todate = fields[45],
                    region_nm_deceased_todate = fields[46],
                    region_nm_vaccinated_1st_todate = fields[47],
                    region_nm_vaccinated_2nd_todate = fields[48],
                    region_po_cases_active = fields[49],
                    region_po_cases_confirmed_todate = fields[50],
                    region_po_deceased_todate = fields[51],
                    region_po_vaccinated_1st_todate = fields[52],
                    region_po_vaccinated_2nd_todate = fields[53],
                    region_sg_cases_active = fields[54],
                    region_sg_cases_confirmed_todate = fields[55],
                    region_sg_deceased_todate = fields[56],
                    region_sg_vaccinated_1st_todate = fields[57],
                    region_sg_vaccinated_2nd_todate = fields[58],
                    region_unknown_cases_active = fields[59],
                    region_unknown_cases_confirmed_todate = fields[60],
                    region_unknown_deceased_todate = fields[61],
                    region_za_cases_active = fields[62],
                    region_za_cases_confirmed_todate = fields[63],
                    region_za_deceased_todate = fields[64],
                    region_za_vaccinated_1st_todate = fields[65],
                    region_za_vaccinated_2nd_todate = fields[66]
                });

                if (count > 10) break;

            }
            file.Close();
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