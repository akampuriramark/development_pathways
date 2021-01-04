using System;
using System.Collections.Generic;

#nullable disable

namespace development_pathways.Models
{
    public partial class County
    {
        public County()
        {
            Applications = new HashSet<Application>();
            SubCounties = new HashSet<SubCounty>();
        }

        public long CountyId { get; set; }
        public string CountyCode { get; set; }
        public string CountyName { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual ICollection<Application> Applications { get; set; }
        public virtual ICollection<SubCounty> SubCounties { get; set; }
    }
}
