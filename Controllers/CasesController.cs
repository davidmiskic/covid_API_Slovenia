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

        // GET: api/Cases/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Case>> GetCase(string id)
        {
            var @case = await _context.Cases.FindAsync(id);

            if (@case == null)
            {
                return NotFound();
            }

            return @case;
        }

        [HttpGet("cases")]
        public async Task<ActionResult<IEnumerable<DayCase>>> GetFilter(string region, string from, string to)
        {
            //var @case = await _context.Cases.Where(c => c.Name.Contains(searchTerm)).ToArray());
            List<String> RegionCols = new List<String>();
            foreach (string cname in colnames)
            {
                if (cname.Contains(region.ToLower())) RegionCols.Add(cname);
            }

            var caseList = await _context.Cases.ToListAsync();
            return await _context.Cases
                // select the data you want directly
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

        [HttpGet("lastweek")]
        public async Task<ActionResult<Case>> GetWeek()
        {
            return NotFound();
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

        private bool CaseExists(string id)
        {
            return _context.Cases.Any(e => e.Id == id);
        }

        string[] colnames = { "Id", "region_ce_cases_active", "region_ce_cases_confirmed_todate", "region_ce_deceased_todate", "region_ce_vaccinated_1st_todate", "region_ce_vaccinated_2nd_todate",
            "region_foreign_cases_active", "region_foreign_cases_confirmed_todate", "region_foreign_deceased_todate", "region_kk_cases_active", "region_kk_cases_confirmed_todate",
            "region_kk_deceased_todate", "region_kk_vaccinated_1st_todate", "region_kk_vaccinated_2nd_todate", "region_kp_cases_active", "region_kp_cases_confirmed_todate",
            "region_kp_deceased_todate", "region_kp_vaccinated_1st_todate", "region_kp_vaccinated_2nd_todate", "region_kr_cases_active", "region_kr_cases_confirmed_todate",
            "region_kr_deceased_todate", "region_kr_vaccinated_1st_todate", "region_kr_vaccinated_2nd_todate", "region_lj_cases_active", "region_lj_cases_confirmed_todate",
            "region_lj_deceased_todate", "region_lj_vaccinated_1st_todate", "region_lj_vaccinated_2nd_todate", "region_mb_cases_active", "region_mb_cases_confirmed_todate",
            "region_mb_deceased_todate", "region_mb_vaccinated_1st_todate", "region_mb_vaccinated_2nd_todate", "region_ms_cases_active", "region_ms_cases_confirmed_todate",
            "region_ms_deceased_todate", "region_ms_vaccinated_1st_todate", "region_ms_vaccinated_2nd_todate", "region_ng_cases_active", "region_ng_cases_confirmed_todate",
            "region_ng_deceased_todate", "region_ng_vaccinated_1st_todate", "region_ng_vaccinated_2nd_todate", "region_nm_cases_active", "region_nm_cases_confirmed_todate",
            "region_nm_deceased_todate", "region_nm_vaccinated_1st_todate", "region_nm_vaccinated_2nd_todate", "region_po_cases_active", "region_po_cases_confirmed_todate",
            "region_po_deceased_todate", "region_po_vaccinated_1st_todate", "region_po_vaccinated_2nd_todate", "region_sg_cases_active", "region_sg_cases_confirmed_todate",
            "region_sg_deceased_todate", "region_sg_vaccinated_1st_todate", "region_sg_vaccinated_2nd_todate", "region_unknown_cases_active", "region_unknown_cases_confirmed_todate",
            "region_unknown_deceased_todate", "region_za_cases_active", "region_za_cases_confirmed_todate", "region_za_deceased_todate", "region_za_vaccinated_1st_todate",
            "region_za_vaccinated_2nd_todate" };
    }
}
