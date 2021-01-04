using development_pathways.Data;
using development_pathways.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace development_pathways.BusinessLogic
{
    public class ApplicationBusinessLogic
    {
        private readonly development_pathways_dbContext _context;

        public ApplicationBusinessLogic(development_pathways_dbContext context)
        {
            _context = context;
        }
        public IQueryable<Application> SearchApplications(ApplicationSearchModel searchModel)
        {
            var result = _context.Applications.Include(a => a.CountyNavigation).Include(a => a.LocationNavigation).Include(a => a.SubCountyNavigation).Include(a => a.SubLocationNavigation).Include(a => a.VillageNavigation).AsQueryable();
            if (searchModel != null)
            {
                if (searchModel.ApplicationId.HasValue)
                    result = result.Where(x => x.ApplicationId == searchModel.ApplicationId);
                if (searchModel.County.HasValue)
                    result = result.Where(x => x.County == searchModel.County);
                if (searchModel.SubCounty.HasValue)
                    result = result.Where(x => x.SubCounty == searchModel.SubCounty);
                if (searchModel.Location.HasValue)
                    result = result.Where(x => x.Location == searchModel.Location);
                if (searchModel.SubLocation.HasValue)
                    result = result.Where(x => x.SubLocation == searchModel.SubLocation);
                if (!string.IsNullOrEmpty(searchModel.FullName))
                    result = result.Where(x => x.FullName.Contains(searchModel.FullName));
            }
            return result;
        }
        public IEnumerable<Application> GetAllApplications()
        {
            return _context.Applications.Include(a => a.CountyNavigation).Include(a => a.LocationNavigation).Include(a => a.SubCountyNavigation).Include(a => a.SubLocationNavigation).Include(a => a.VillageNavigation);

        }

        public async Task<int> SaveApplication(Application application)
        {
            _context.Add(application);
            return await _context.SaveChangesAsync();
        }
    }
}
