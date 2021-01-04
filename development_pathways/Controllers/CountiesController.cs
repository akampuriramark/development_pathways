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
    public class CountiesController : Controller
    {
        private readonly development_pathways_dbContext _context;

        public CountiesController(development_pathways_dbContext context)
        {
            _context = context;
        }

        // GET: Counties
        public async Task<IActionResult> Index()
        {
            return View(await _context.Counties.ToListAsync());
        }

        // GET: Counties/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var county = await _context.Counties
                .FirstOrDefaultAsync(m => m.CountyId == id);
            if (county == null)
            {
                return NotFound();
            }

            return View(county);
        }

        // GET: Counties/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Counties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CountyId,CountyCode,CountyName,CreatedAt")] County county)
        {
            if (ModelState.IsValid)
            {
                _context.Add(county);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(county);
        }

        // GET: Counties/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var county = await _context.Counties.FindAsync(id);
            if (county == null)
            {
                return NotFound();
            }
            return View(county);
        }

        // POST: Counties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("CountyId,CountyCode,CountyName,CreatedAt")] County county)
        {
            if (id != county.CountyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(county);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountyExists(county.CountyId))
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
            return View(county);
        }

        // GET: Counties/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var county = await _context.Counties
                .FirstOrDefaultAsync(m => m.CountyId == id);
            if (county == null)
            {
                return NotFound();
            }

            return View(county);
        }

        // POST: Counties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var county = await _context.Counties.FindAsync(id);
            _context.Counties.Remove(county);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CountyExists(long id)
        {
            return _context.Counties.Any(e => e.CountyId == id);
        }
    }
}
