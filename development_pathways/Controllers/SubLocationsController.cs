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
    public class SubLocationsController : Controller
    {
        private readonly development_pathways_dbContext _context;

        public SubLocationsController(development_pathways_dbContext context)
        {
            _context = context;
        }

        // GET: SubLocations
        public async Task<IActionResult> Index()
        {
            var development_pathways_dbContext = _context.SubLocations.Include(s => s.Location);
            return View(await development_pathways_dbContext.ToListAsync());
        }

        // GET: SubLocations/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subLocation = await _context.SubLocations
                .Include(s => s.Location)
                .FirstOrDefaultAsync(m => m.SubLocationId == id);
            if (subLocation == null)
            {
                return NotFound();
            }

            return View(subLocation);
        }

        // GET: SubLocations/Create
        public IActionResult Create()
        {
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationCode");
            return View();
        }

        // POST: SubLocations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubLocationId,SubLocationCode,SubLocationName,LocationId,CreatedAt")] SubLocation subLocation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subLocation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationCode", subLocation.LocationId);
            return View(subLocation);
        }

        // GET: SubLocations/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subLocation = await _context.SubLocations.FindAsync(id);
            if (subLocation == null)
            {
                return NotFound();
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationCode", subLocation.LocationId);
            return View(subLocation);
        }

        // POST: SubLocations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("SubLocationId,SubLocationCode,SubLocationName,LocationId,CreatedAt")] SubLocation subLocation)
        {
            if (id != subLocation.SubLocationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subLocation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubLocationExists(subLocation.SubLocationId))
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
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationCode", subLocation.LocationId);
            return View(subLocation);
        }

        // GET: SubLocations/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subLocation = await _context.SubLocations
                .Include(s => s.Location)
                .FirstOrDefaultAsync(m => m.SubLocationId == id);
            if (subLocation == null)
            {
                return NotFound();
            }

            return View(subLocation);
        }

        // POST: SubLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var subLocation = await _context.SubLocations.FindAsync(id);
            _context.SubLocations.Remove(subLocation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubLocationExists(long id)
        {
            return _context.SubLocations.Any(e => e.SubLocationId == id);
        }
    }
}
