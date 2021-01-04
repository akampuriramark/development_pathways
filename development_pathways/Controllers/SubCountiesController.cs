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
    public class SubCountiesController : Controller
    {
        private readonly development_pathways_dbContext _context;

        public SubCountiesController(development_pathways_dbContext context)
        {
            _context = context;
        }

        // GET: SubCounties
        public async Task<IActionResult> Index()
        {
            var development_pathways_dbContext = _context.SubCounties.Include(s => s.County);
            return View(await development_pathways_dbContext.ToListAsync());
        }

        // GET: SubCounties/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCounty = await _context.SubCounties
                .Include(s => s.County)
                .FirstOrDefaultAsync(m => m.SubCountyId == id);
            if (subCounty == null)
            {
                return NotFound();
            }

            return View(subCounty);
        }

        // GET: SubCounties/Create
        public IActionResult Create()
        {
            ViewData["CountyId"] = new SelectList(_context.Counties, "CountyId", "CountyName");
            return View();
        }

        // POST: SubCounties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubCountyId,SubCountyCode,SubCountyName,CountyId,CreatedAt")] SubCounty subCounty)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subCounty);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountyId"] = new SelectList(_context.Counties, "CountyId", "CountyName", subCounty.CountyId);
            return View(subCounty);
        }

        // GET: SubCounties/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCounty = await _context.SubCounties.FindAsync(id);
            if (subCounty == null)
            {
                return NotFound();
            }
            ViewData["CountyId"] = new SelectList(_context.Counties, "CountyId", "CountyName", subCounty.CountyId);
            return View(subCounty);
        }

        // POST: SubCounties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("SubCountyId,SubCountyCode,SubCountyName,CountyId,CreatedAt")] SubCounty subCounty)
        {
            if (id != subCounty.SubCountyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subCounty);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubCountyExists(subCounty.SubCountyId))
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
            ViewData["CountyId"] = new SelectList(_context.Counties, "CountyId", "CountyName", subCounty.CountyId);
            return View(subCounty);
        }

        // GET: SubCounties/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCounty = await _context.SubCounties
                .Include(s => s.County)
                .FirstOrDefaultAsync(m => m.SubCountyId == id);
            if (subCounty == null)
            {
                return NotFound();
            }

            return View(subCounty);
        }

        // POST: SubCounties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var subCounty = await _context.SubCounties.FindAsync(id);
            _context.SubCounties.Remove(subCounty);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubCountyExists(long id)
        {
            return _context.SubCounties.Any(e => e.SubCountyId == id);
        }
    }
}
