using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyProject.Data;
using PhilatelistOnMVC.Models;

namespace PhilatelistOnMVC.Controllers
{
    [Authorize]
    
    public class PhilatelistsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PhilatelistsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Philatelists
        

        // GET: Philatelists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var philatelist = await _context.Philatelist
                .FirstOrDefaultAsync(m => m.Id == id);
            if (philatelist == null)
            {
                return NotFound();
            }

            return View(philatelist);
        }

        // GET: Philatelists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Philatelists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Country,ContactCoordinates,Availability")] Philatelist philatelist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(philatelist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(philatelist);
        }

        // GET: Philatelists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var philatelist = await _context.Philatelist.FindAsync(id);
            if (philatelist == null)
            {
                return NotFound();
            }
            return View(philatelist);
        }

        // POST: Philatelists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Country,ContactCoordinates,Availability")] Philatelist philatelist)
        {
            if (id != philatelist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(philatelist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhilatelistExists(philatelist.Id))
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
            return View(philatelist);
        }

        // GET: Philatelists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var philatelist = await _context.Philatelist
                .FirstOrDefaultAsync(m => m.Id == id);
            if (philatelist == null)
            {
                return NotFound();
            }

            return View(philatelist);
        }

        // POST: Philatelists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var philatelist = await _context.Philatelist.FindAsync(id);
            _context.Philatelist.Remove(philatelist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhilatelistExists(int id)
        {
            return _context.Philatelist.Any(e => e.Id == id);
        }


        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            var philatelists = from z in _context.Philatelist
                        select z;

            ViewBag.Name1SortParm = String.IsNullOrEmpty(sortOrder) ? "Name1_desc" : "";
            ViewBag.Country1SortParm = sortOrder == "Country1" ? "Country1_desc" : "Country1";
            ViewBag.ContactCoordinatesSortParm = sortOrder == "ContactCoordinates" ? "ContactCoordinates_desc" : "ContactCoordinates";
            ViewBag.AvailabilitySortParm = sortOrder == "Availability" ? "Availability_desc" : "Availability";
         

            switch (sortOrder)
            {
                case "Name1_desc":
                    philatelists = philatelists.OrderByDescending(s => s.Name);
                    break;
                case "Country1":
                    philatelists = philatelists.OrderBy(s => s.Country);
                    break;
                case "Country1_desc":
                    philatelists = philatelists.OrderByDescending(s => s.Country);
                    break;
                case "ContactCoordinates":
                    philatelists = philatelists.OrderBy(s => s.ContactCoordinates);
                    break;
                case "ContactCoordinates_desc":
                    philatelists = philatelists.OrderByDescending(s => s.ContactCoordinates);
                    break;
                case "Availability":
                    philatelists = philatelists.OrderBy(s => s.Availability);
                    break;
                case "Availability_desc":
                    philatelists = philatelists.OrderByDescending(s => s.Availability);
                    break;
                default:
                    philatelists = philatelists.OrderBy(s => s.Name);
                    break;
            }


            if (!String.IsNullOrEmpty(searchString))
            {
                philatelists = philatelists.Where(s => s.Name.Contains(searchString) || s.Country.Contains(searchString) || s.ContactCoordinates.Contains(searchString) || s.Availability.Contains(searchString));
                                
            }

            return View(await philatelists.ToListAsync());
        }
    }
}
