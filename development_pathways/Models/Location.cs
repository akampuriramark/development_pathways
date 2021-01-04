using System;
using System.Collections.Generic;

#nullable disable

namespace development_pathways.Models
{
    public partial class Location
    {
        public Location()
        {
            Applications = new HashSet<Application>();
            SubLocations = new HashSet<SubLocation>();
        }

        public long LocationId { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }
        public long SubCountyId { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual SubCounty SubCounty { get; set; }
        public virtual ICollection<Application> Applications { get; set; }
        public virtual ICollection<SubLocation> SubLocations { get; set; }
    }
}
