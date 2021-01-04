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
    public class TelephoneContactsController : Controller
    {
        private readonly development_pathways_dbContext _context;

        public TelephoneContactsController(development_pathways_dbContext context)
        {
            _context = context;
        }

        // GET: TelephoneContacts
        public async Task<IActionResult> Index()
        {
            var development_pathways_dbContext = _context.TelephoneContacts.Include(t => t.Application);
            return View(await development_pathways_dbContext.ToListAsync());
        }

        // GET: TelephoneContacts/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var telephoneContact = await _context.TelephoneContacts
                .Include(t => t.Application)
                .FirstOrDefaultAsync(m => m.TelephoneContactId == id);
            if (telephoneContact == null)
            {
                return NotFound();
            }

            return View(telephoneContact);
        }

        // GET: TelephoneContacts/Create
        public IActionResult Create()
        {
            ViewData["ApplicationId"] = new SelectList(_context.Applications, "ApplicationId", "FullName");
            return View();
        }

        // POST: TelephoneContacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TelephoneContactId,TelephoneContact1,ApplicationId,CreatedAt")] TelephoneContact telephoneContact)
        {
            if (ModelState.IsValid)
            {
                _context.Add(telephoneContact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationId"] = new SelectList(_context.Applications, "ApplicationId", "FullName", telephoneContact.ApplicationId);
            return View(telephoneContact);
        }

        // GET: TelephoneContacts/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var telephoneContact = await _context.TelephoneContacts.FindAsync(id);
            if (telephoneContact == null)
            {
                return NotFound();
            }
            ViewData["ApplicationId"] = new SelectList(_context.Applications, "ApplicationId", "FullName", telephoneContact.ApplicationId);
            return View(telephoneContact);
        }

        // POST: TelephoneContacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("TelephoneContactId,TelephoneContact1,ApplicationId,CreatedAt")] TelephoneContact telephoneContact)
        {
            if (id != telephoneContact.TelephoneContactId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(telephoneContact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TelephoneContactExists(telephoneContact.TelephoneContactId))
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
            ViewData["ApplicationId"] = new SelectList(_context.Applications, "ApplicationId", "FullName", telephoneContact.ApplicationId);
            return View(telephoneContact);
        }

        // GET: TelephoneContacts/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var telephoneContact = await _context.TelephoneContacts
                .Include(t => t.Application)
                .FirstOrDefaultAsync(m => m.TelephoneContactId == id);
            if (telephoneContact == null)
            {
                return NotFound();
            }

            return View(telephoneContact);
        }

        // POST: TelephoneContacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var telephoneContact = await _context.TelephoneContacts.FindAsync(id);
            _context.TelephoneContacts.Remove(telephoneContact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TelephoneContactExists(long id)
        {
            return _context.TelephoneContacts.Any(e => e.TelephoneContactId == id);
        }
    }
}
