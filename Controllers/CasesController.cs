using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CovidAPI.Models;

namespace covidAPI.Controllers
{
    [Route("/api/region/")]
    [ApiController]
    public class CasesController : ControllerBase
    {
        private readonly CaseContext _context;

        public CasesController(CaseContext context)
        {
            _context = context;
        }

        // GET: api/Cases
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Case>>> GetCases()
        {
            return await _context.Cases.ToListAsync();
        }

        [HttpGet("{date}")]
        public async Task<ActionResult<Case>> GetCase(string id)
        {
            var @case = await _context.Cases.FindAsync(DateTime.Parse(id).Date);

            if (@case == null)
            {
                return NotFound();
            }

            return @case;
        }

        [HttpGet("cases")]
        public async Task<ActionResult<IEnumerable<DayCase>>> GetFilter(string region, string from, string to)
        {
            if (region == null) return BadRequest(); //nothing is possible
            if (from == null && to == null) //give all region data
            {
                return await _context.Cases
                .Select(dc => new DayCase
                {
                    Id = dc.Id,
                    Region = region.ToUpper(),
                    ActiveDay = typeof(Case).GetProperty("region_" + region.ToLower() + "_cases_active").GetValue(dc).ToString(),
                    Vac1st = typeof(Case).GetProperty("region_" + region.ToLower() + "_vaccinated_1st_todate").GetValue(dc).ToString(),
                    Vac2nd = typeof(Case).GetProperty("region_" + region.ToLower() + "_vaccinated_2nd_todate").GetValue(dc).ToString(),
                    Deceased = typeof(Case).GetProperty("region_" + region.ToLower() + "_deceased_todate").GetValue(dc).ToString()
                }).ToListAsync();
            }
            else if (from != null && to != null) //give data between two dates
            {
                return await _context.Cases.Where(c => DateTime.Compare(c.Id, DateTime.Parse(from).Date) >= 0 && DateTime.Compare(c.Id, DateTime.Parse(to).Date) <= 0)
                    .Select(dc => new DayCase
                    {
                        Id = dc.Id,
                        Region = region.ToUpper(),
                        ActiveDay = typeof(Case).GetProperty("region_" + region.ToLower() + "_cases_active").GetValue(dc).ToString(),
                        Vac1st = typeof(Case).GetProperty("region_" + region.ToLower() + "_vaccinated_1st_todate").GetValue(dc).ToString(),
                        Vac2nd = typeof(Case).GetProperty("region_" + region.ToLower() + "_vaccinated_2nd_todate").GetValue(dc).ToString(),
                        Deceased = typeof(Case).GetProperty("region_" + region.ToLower() + "_deceased_todate").GetValue(dc).ToString()
                    }).ToListAsync();
            }
            else if (from != null)
            {
                return await _context.Cases.Where(c => DateTime.Compare(c.Id, DateTime.Parse(from).Date) >= 0)
                    .Select(dc => new DayCase
                    {
                        Id = dc.Id,
                        Region = region.ToUpper(),
                        ActiveDay = typeof(Case).GetProperty("region_" + region.ToLower() + "_cases_active").GetValue(dc).ToString(),
                        Vac1st = typeof(Case).GetProperty("region_" + region.ToLower() + "_vaccinated_1st_todate").GetValue(dc).ToString(),
                        Vac2nd = typeof(Case).GetProperty("region_" + region.ToLower() + "_vaccinated_2nd_todate").GetValue(dc).ToString(),
                        Deceased = typeof(Case).GetProperty("region_" + region.ToLower() + "_deceased_todate").GetValue(dc).ToString()
                    }).ToListAsync();
            }
            else if (to != null)
            {
                return await _context.Cases.Where(c => DateTime.Compare(c.Id, DateTime.Parse(to).Date) <= 0)
                    .Select(dc => new DayCase
                    {
                        Id = dc.Id,
                        Region = region.ToUpper(),
                        ActiveDay = typeof(Case).GetProperty("region_" + region.ToLower() + "_cases_active").GetValue(dc).ToString(),
                        Vac1st = typeof(Case).GetProperty("region_" + region.ToLower() + "_vaccinated_1st_todate").GetValue(dc).ToString(),
                        Vac2nd = typeof(Case).GetProperty("region_" + region.ToLower() + "_vaccinated_2nd_todate").GetValue(dc).ToString(),
                        Deceased = typeof(Case).GetProperty("region_" + region.ToLower() + "_deceased_todate").GetValue(dc).ToString()
                    }).ToListAsync();
            }
            else return BadRequest();
        }

        [HttpGet("lastweek")]
        public async Task<ActionResult<IEnumerable<RegionSummary>>> GetWeek()
        {
            DateTime from = DateTime.Now.AddDays(-7).Date;
            List<RegionSummary> regionweeks = new List<RegionSummary>();
            string[] possibleRegions = { "lj", "ce", "kr", "nm", "kk", "kp", "mb", "ms", "ng", "po", "sg", "za" };

            foreach (string region in possibleRegions) //get all entries of last week
            {
                List<DayCase> week = await _context.Cases.Where(c => DateTime.Compare(c.Id, from) >= 0)
                    .Select(dc => new DayCase
                    {
                        Id = dc.Id,
                        ActiveDay = typeof(Case).GetProperty("region_" + region.ToLower() + "_cases_active").GetValue(dc).ToString(),
                        Vac1st = "",
                        Vac2nd = "",
                        Deceased = ""
                    }).ToListAsync();
                int active = 0;
                foreach(DayCase dc in week)
                {
                    try { active = active + int.Parse(dc.ActiveDay); }
                    catch { }
                }
                regionweeks.Add(new RegionSummary
                {
                    Id = region,
                    Active = active,
                    from = from.Date
                });
            }
            regionweeks = regionweeks.OrderByDescending(o => o.Active).ToList();
            return regionweeks;
        }

        //// PUT: api/Cases/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCase(string id, Case @case)
        //{
        //    if (id != @case.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(@case).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CaseExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Cases
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Case>> PostCase(Case @case)
        //{
        //    _context.Cases.Add(@case);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (CaseExists(@case.Id))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetCase", new { id = @case.Id }, @case);
        //}

        //// DELETE: api/Cases/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCase(string id)
        //{
        //    var @case = await _context.Cases.FindAsync(id);
        //    if (@case == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Cases.Remove(@case);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool CaseExists(string id)
        //{
        //    return _context.Cases.Any(e => e.Id == id);
        //}
    }
}
