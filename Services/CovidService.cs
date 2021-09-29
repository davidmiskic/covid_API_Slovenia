using CovidAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;

//this makes in memory DB

namespace CovidAPI.Services
{
    public static class CovidService
    {
        static List<Case> Cases { get; }
        static int nextId = 3;

        static CovidService()
        {
            Cases = new List<Case>
            {
                new Case { Date = "2020-05-12", Region = "Lj2" },
                new Case { Date = "2020-05-15", Region = "Lj2" }
            };
        }

        public static void Init()
        {
            var client = new WebClient();
            client.DownloadFile("https://raw.githubusercontent.com/sledilnik/data/master/csv/region-cases.csv", "db.csv");
            // TODO read csv and make a List
        }

        public static List<Case> GetAll() => Cases;

        public static Case Get(string id) => Cases.FirstOrDefault(p => p.Date == id);

        public static void Add(Case c)
        {
            c.Date = System.DateTime.Now.AddDays(nextId++).ToShortDateString();
            Cases.Add(c);
        }

        public static void Delete(string id)
        {
            var c = Get(id);
            if (c is null)
                return;

            Cases.Remove(c);
        }

        public static void Update(Case c)
        {
            var index = Cases.FindIndex(p => p.Date == c.Date);
            if (index == -1)
                return;

            Cases[index] = c;
        }
    }
}