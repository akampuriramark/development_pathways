using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using development_pathways.Data;
using development_pathways.Models;

namespace development_pathways.Controllers
{
    public class LocationsController : Controller
    {
        private readonly development_pathways_dbContext _context;

        public LocationsController(development_pathways_dbContext context)
        {
            _context = context;
        }

        // GET: Locations
        public async Task<IActionResult> Index()
        {
            var development_pathways_dbContext = _context.Locations.Include(l => l.SubCounty);
            return View(await development_pathways_dbContext.ToListAsync());
        }

        // GET: Locations/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Locations
                .Include(l => l.SubCounty)
                .FirstOrDefaultAsync(m => m.LocationId == id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // GET: Locations/Create
        public IActionResult Create()
        {
            ViewData["SubCountyId"] = new SelectList(_context.SubCounties, "SubCountyId", "SubCountyCode");
            return View();
        }

        // POST: Locations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LocationId,LocationCode,LocationName,SubCountyId,CreatedAt")] Location location)
        {
            if (ModelState.IsValid)
            {
                _context.Add(location);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubCountyId"] = new SelectList(_context.SubCounties, "SubCountyId", "SubCountyCode", location.SubCountyId);
            return View(location);
        }

        // GET: Locations/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Locations.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }
            ViewData["SubCountyId"] = new SelectList(_context.SubCounties, "SubCountyId", "SubCountyCode", location.SubCountyId);
            return View(location);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("LocationId,LocationCode,LocationName,SubCountyId,CreatedAt")] Location location)
        {
            if (id != location.LocationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(location);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationExists(location.LocationId))
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
            ViewData["SubCountyId"] = new SelectList(_context.SubCounties, "SubCountyId", "SubCountyCode", location.SubCountyId);
            return View(location);
        }

        // GET: Locations/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Locations
                .Include(l => l.SubCounty)
                .FirstOrDefaultAsync(m => m.LocationId == id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var location = await _context.Locations.FindAsync(id);
            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocationExists(long id)
        {
            return _context.Locations.Any(e => e.LocationId == id);
        }
    }
}
