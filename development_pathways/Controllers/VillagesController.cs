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
    public class VillagesController : Controller
    {
        private readonly development_pathways_dbContext _context;

        public VillagesController(development_pathways_dbContext context)
        {
            _context = context;
        }

        // GET: Villages
        public async Task<IActionResult> Index()
        {
            var development_pathways_dbContext = _context.Villages.Include(v => v.SubLocation);
            return View(await development_pathways_dbContext.ToListAsync());
        }

        // GET: Villages/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var village = await _context.Villages
                .Include(v => v.SubLocation)
                .FirstOrDefaultAsync(m => m.VillageId == id);
            if (village == null)
            {
                return NotFound();
            }

            return View(village);
        }

        // GET: Villages/Create
        public IActionResult Create()
        {
            ViewData["SubLocationId"] = new SelectList(_context.SubLocations, "SubLocationId", "SubLocationCode");
            return View();
        }

        // POST: Villages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VillageId,VillageCode,VillageName,SubLocationId,CreatedAt")] Village village)
        {
            if (ModelState.IsValid)
            {
                _context.Add(village);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubLocationId"] = new SelectList(_context.SubLocations, "SubLocationId", "SubLocationCode", village.SubLocationId);
            return View(village);
        }

        // GET: Villages/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var village = await _context.Villages.FindAsync(id);
            if (village == null)
            {
                return NotFound();
            }
            ViewData["SubLocationId"] = new SelectList(_context.SubLocations, "SubLocationId", "SubLocationCode", village.SubLocationId);
            return View(village);
        }

        // POST: Villages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("VillageId,VillageCode,VillageName,SubLocationId,CreatedAt")] Village village)
        {
            if (id != village.VillageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(village);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VillageExists(village.VillageId))
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
            ViewData["SubLocationId"] = new SelectList(_context.SubLocations, "SubLocationId", "SubLocationCode", village.SubLocationId);
            return View(village);
        }

        // GET: Villages/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var village = await _context.Villages
                .Include(v => v.SubLocation)
                .FirstOrDefaultAsync(m => m.VillageId == id);
            if (village == null)
            {
                return NotFound();
            }

            return View(village);
        }

        // POST: Villages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var village = await _context.Villages.FindAsync(id);
            _context.Villages.Remove(village);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VillageExists(long id)
        {
            return _context.Villages.Any(e => e.VillageId == id);
        }
    }
}
