using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CovidAPI.Models;
using CovidAPI.Services;

namespace covidAPI.Controllers
{
    [ApiController]
    [Route("/api/region/")] // in this case CovidCase
    public class CovidCaseController : ControllerBase
    {

        private static readonly string[] Regions = new[]
        {
            "Lj", "Mb", "Kp", "NM"
        };

        private readonly ILogger<CovidCaseController> _logger;

        public CovidCaseController(ILogger<CovidCaseController> logger)
        {
            _logger = logger;
            CovidService.Init();
        }

        [HttpGet]
        public ActionResult<List<Case>> GetAll() => CovidService.GetAll();
        //public IEnumerable<Case> Get()
        //{
        //    var rng = new Random();
        //    return Enumerable.Range(1, 5).Select(index => new Case
        //    {
        //        Date = DateTime.Now.AddDays(index).ToShortDateString(),
        //        //TemperatureC = rng.Next(-20, 55),
        //        Region = Regions[rng.Next(Regions.Length)]
        //    })
        //    .ToArray();
        //}

        [HttpGet("{id}")]
        public ActionResult<Case> GetOne(string id)
        {
            var c = CovidService.Get(id);

            if (c == null)
                return NotFound();

            return c;
        }

        [HttpGet("cases/{region}/{from}/{to}")]
        public ActionResult<Case> GetFilter(string region, string from, string to)
        {
            return NotFound();
        }

        [HttpGet("lastweek")]
        public ActionResult<Case> GetWeek()
        {
            return NotFound();
        }


    }
}
