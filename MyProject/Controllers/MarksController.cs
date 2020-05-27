using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyProject.Data;
using MyProject.Models;

namespace MyProject.Controllers
{
    [Authorize]
    public class MarksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MarksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Marks
        

        // GET: Marks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mark = await _context.Mark
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mark == null)
            {
                return NotFound();
            }

            return View(mark);
        }
        //------------------------------------------------------
        

        //-------------------------------------------------

        // GET: Marks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Marks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Country,NominalValue,Year,Count,Features")] Mark mark)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mark);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mark);
        }

        // GET: Marks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mark = await _context.Mark.FindAsync(id);
            if (mark == null)
            {
                return NotFound();
            }
            return View(mark);
        }

        // POST: Marks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Country,NominalValue,Year,Count,Features")] Mark mark)
        {
            if (id != mark.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mark);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MarkExists(mark.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(mark);
        }

        // GET: Marks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mark = await _context.Mark
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mark == null)
            {
                return NotFound();
            }

            return View(mark);
        }

        // POST: Marks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mark = await _context.Mark.FindAsync(id);
            _context.Mark.Remove(mark);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MarkExists(int id)
        {
            return _context.Mark.Any(e => e.Id == id);
        }
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            var marks = from m in _context.Mark
                        select m;

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
            ViewBag.CountrySortParm = sortOrder == "Country" ? "Country_desc" : "Country";
            ViewBag.NominalValueSortParm = sortOrder == "NominalValue" ? "NominalValue_desc" : "NominalValue";
            ViewBag.YearSortParm = sortOrder == "Year" ? "Year_desc" : "Year";
            ViewBag.CountSortParm = sortOrder == "Count" ? "Count_desc" : "Count";
            ViewBag.FeaturesSortParm = sortOrder == "Features" ? "Features_desc" : "Features";

            switch (sortOrder)
            {
                case "Name_desc":
                    marks = marks.OrderByDescending(s => s.Name);
                    break;
                case "Country":
                    marks = marks.OrderBy(s => s.Country);
                    break;
                case "Country_desc":
                    marks = marks.OrderByDescending(s => s.Country);
                    break;
                case "NominalValue":
                    marks = marks.OrderBy(s => s.NominalValue);
                    break;
                case "NominalValue_desc":
                    marks = marks.OrderByDescending(s => s.NominalValue);
                    break;
                case "Year":
                    marks = marks.OrderBy(s => s.Year);
                    break;
                case "Year_desc":
                    marks = marks.OrderByDescending(s => s.Year);
                    break;
                case "Count":
                    marks = marks.OrderBy(s => s.Count);
                    break;
                case "Count_desc":
                    marks = marks.OrderByDescending(s => s.Count);
                    break;
                case "Features":
                    marks = marks.OrderBy(s => s.Features);
                    break;
                case "Features_desc":
                    marks = marks.OrderByDescending(s => s.Features);
                    break;
                default:
                    marks = marks.OrderBy(s => s.Name);
                    break;
            }


            if (!String.IsNullOrEmpty(searchString))
            {
                marks = marks.Where(s => s.Name.Contains(searchString) || s.Country.Contains(searchString) || s.NominalValue.Contains(searchString) || s.Year.Contains(searchString)
                                || s.Count.Contains(searchString) || s.Features.Contains(searchString));
            }

            return View(await marks.ToListAsync());
        }
    }
    
}
