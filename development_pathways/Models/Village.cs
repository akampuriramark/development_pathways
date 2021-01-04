using System;
using System.Collections.Generic;

#nullable disable

namespace development_pathways.Models
{
    public partial class Village
    {
        public Village()
        {
            Applications = new HashSet<Application>();
        }

        public long VillageId { get; set; }
        public string VillageCode { get; set; }
        public string VillageName { get; set; }
        public long SubLocationId { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual SubLocation SubLocation { get; set; }
        public virtual ICollection<Application> Applications { get; set; }
    }
}
