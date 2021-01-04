using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace development_pathways.Models
{
    public class ApplicationViewModel
    {
        public List<Application> Applications { get; set; }
        public ApplicationSearchModel ApplicationSearchModel { get; set; }
    }
}
