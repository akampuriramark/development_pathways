using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace development_pathways.Models
{
    public class ApplicationSearchModel
    {
        [DisplayName("Application ID")]
        public long? ApplicationId { get; set; }
        [DisplayName("Full Name")]
        public string FullName { get; set; }
        [DisplayName("County")]
        public long? County { get; set; }
        public long? Location { get; set; }
        public long? SubCounty { get; set; }
        public long? SubLocation { get; set; }
        public virtual County CountyNavigation { get; set; }
        [DisplayName("Sub County")]
        public virtual SubCounty SubCountyNavigation { get; set; }
        [DisplayName("Location")]
        public virtual Location LocationNavigation { get; set; }
        [DisplayName("Sub Location")]
        public virtual SubLocation SubLocationNavigation { get; set; }
    }
}
