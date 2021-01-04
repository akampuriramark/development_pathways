using System;
using System.Collections.Generic;

#nullable disable

namespace development_pathways.Models
{
    public partial class SubCounty
    {
        public SubCounty()
        {
            Applications = new HashSet<Application>();
            Locations = new HashSet<Location>();
        }

        public long SubCountyId { get; set; }
        public string SubCountyCode { get; set; }
        public string SubCountyName { get; set; }
        public long CountyId { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual County County { get; set; }
        public virtual ICollection<Application> Applications { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
    }
}
