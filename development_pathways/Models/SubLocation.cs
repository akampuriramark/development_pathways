using System;
using System.Collections.Generic;

#nullable disable

namespace development_pathways.Models
{
    public partial class SubLocation
    {
        public SubLocation()
        {
            Applications = new HashSet<Application>();
            Villages = new HashSet<Village>();
        }

        public long SubLocationId { get; set; }
        public string SubLocationCode { get; set; }
        public string SubLocationName { get; set; }
        public long LocationId { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Location Location { get; set; }
        public virtual ICollection<Application> Applications { get; set; }
        public virtual ICollection<Village> Villages { get; set; }
    }
}
