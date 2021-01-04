using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using development_pathways.Data;
using development_pathways.Models;
using development_pathways.BusinessLogic;

namespace development_pathways.Controllers
{
    public class ApplicationsController : Controller
    {
        private readonly development_pathways_dbContext _context;

        public ApplicationsController(development_pathways_dbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ApplicationViewModel viewModel)
        {
            ApplicationViewModel mymodel = new ApplicationViewModel();
            ApplicationSearchModel searchModel = viewModel.ApplicationSearchModel;
            var business = new ApplicationBusinessLogic(_context);
            var model = business.SearchApplications(searchModel);
            mymodel.Applications = model.ToList();
            mymodel.ApplicationSearchModel = new ApplicationSearchModel();

            ViewData["County"] = new SelectList(_context.Counties, "CountyId", "CountyName");
            ViewData["Location"] = new SelectList(_context.Locations, "LocationId", "LocationName");
            ViewData["SubCounty"] = new SelectList(_context.SubCounties, "SubCountyId", "SubCountyName");
            ViewData["SubLocation"] = new SelectList(_context.SubLocations, "SubLocationId", "SubLocationName");
            ViewData["Village"] = new SelectList(_context.Villages, "VillageId", "VillageName");

            return View(mymodel);
        }

        public ActionResult Index()
        {
            ApplicationViewModel mymodel = new ApplicationViewModel();

            var business = new ApplicationBusinessLogic(_context);
            var model = business.GetAllApplications();
            mymodel.Applications = model.ToList();
            mymodel.ApplicationSearchModel = new ApplicationSearchModel();

            ViewData["County"] = new SelectList(_context.Counties, "CountyId", "CountyName");
            ViewData["Location"] = new SelectList(_context.Locations, "LocationId", "LocationName");
            ViewData["SubCounty"] = new SelectList(_context.SubCounties, "SubCountyId", "SubCountyName");
            ViewData["SubLocation"] = new SelectList(_context.SubLocations, "SubLocationId", "SubLocationName");
            ViewData["Village"] = new SelectList(_context.Villages, "VillageId", "VillageName");

            return View(mymodel);
        }

        // GET: Applications/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications
                .Include(a => a.CountyNavigation)
                .Include(a => a.LocationNavigation)
                .Include(a => a.SubCountyNavigation)
                .Include(a => a.SubLocationNavigation)
                .Include(a => a.VillageNavigation)
                .Include(a => a.TelephoneContacts)
                .FirstOrDefaultAsync(m => m.ApplicationId == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // GET: Applications/Create
        public IActionResult Create()
        {
            ViewData["County"] = new SelectList(_context.Counties, "CountyId", "CountyName");
            ViewData["Location"] = new SelectList(_context.Locations, "LocationId", "LocationName");
            ViewData["SubCounty"] = new SelectList(_context.SubCounties, "SubCountyId", "SubCountyName");
            ViewData["SubLocation"] = new SelectList(_context.SubLocations, "SubLocationId", "SubLocationName");
            ViewData["Village"] = new SelectList(_context.Villages, "VillageId", "VillageName");
            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApplicationId,FullName,DateOfBirth,MaritalStatus,IdNumber,County,SubCounty,Location,SubLocation,Village,PostalAddress,PhysicalAddress,PoorElderlyPersons,OrphanAndVulnerableChildren,PersonsWithDisability,PersonsInExtremePoverty,AnyOther,InfoCollectedBy,DesignationOfCollector,DateOfCollection,CreatedAt")] Application application)
        {
            if (ModelState.IsValid)
            {
                var business = new ApplicationBusinessLogic(_context);
                await business.SaveApplication(application);
                return RedirectToAction(nameof(Index));
            }
            ViewData["County"] = new SelectList(_context.Counties, "CountyId", "CountyName", application.County);
            ViewData["Location"] = new SelectList(_context.Locations, "LocationId", "LocationName", application.Location);
            ViewData["SubCounty"] = new SelectList(_context.SubCounties, "SubCountyId", "SubCountyName", application.SubCounty);
            ViewData["SubLocation"] = new SelectList(_context.SubLocations, "SubLocationId", "SubLocationName", application.SubLocation);
            ViewData["Village"] = new SelectList(_context.Villages, "VillageId", "VillageName", application.Village);
            return View(application);
        }

        // GET: Applications/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }
            ViewData["County"] = new SelectList(_context.Counties, "CountyId", "CountyName", application.County);
            ViewData["Location"] = new SelectList(_context.Locations, "LocationId", "LocationName", application.Location);
            ViewData["SubCounty"] = new SelectList(_context.SubCounties, "SubCountyId", "SubCountyName", application.SubCounty);
            ViewData["SubLocation"] = new SelectList(_context.SubLocations, "SubLocationId", "SubLocationName", application.SubLocation);
            ViewData["Village"] = new SelectList(_context.Villages, "VillageId", "VillageName", application.Village);
            return View(application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("ApplicationId,FullName,DateOfBirth,MaritalStatus,IdNumber,County,SubCounty,Location,SubLocation,Village,PostalAddress,PhysicalAddress,PoorElderlyPersons,OrphanAndVulnerableChildren,PersonsWithDisability,PersonsInExtremePoverty,AnyOther,InfoCollectedBy,DesignationOfCollector,DateOfCollection,CreatedAt")] Application application)
        {
            if (id != application.ApplicationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(application);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationExists(application.ApplicationId))
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
            ViewData["County"] = new SelectList(_context.Counties, "CountyId", "CountyName", application.County);
            ViewData["Location"] = new SelectList(_context.Locations, "LocationId", "LocationName", application.Location);
            ViewData["SubCounty"] = new SelectList(_context.SubCounties, "SubCountyId", "SubCountyName", application.SubCounty);
            ViewData["SubLocation"] = new SelectList(_context.SubLocations, "SubLocationId", "SubLocationName", application.SubLocation);
            ViewData["Village"] = new SelectList(_context.Villages, "VillageId", "VillageName", application.Village);
            return View(application);
        }

        // GET: Applications/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications
                .Include(a => a.CountyNavigation)
                .Include(a => a.LocationNavigation)
                .Include(a => a.SubCountyNavigation)
                .Include(a => a.SubLocationNavigation)
                .Include(a => a.VillageNavigation)
                .FirstOrDefaultAsync(m => m.ApplicationId == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var application = await _context.Applications.FindAsync(id);
            _context.Applications.Remove(application);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationExists(long id)
        {
            return _context.Applications.Any(e => e.ApplicationId == id);
        }
    }
}
