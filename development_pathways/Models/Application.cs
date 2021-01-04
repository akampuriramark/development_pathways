using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace development_pathways.Models
{
    public partial class Application
    {
        public Application()
        {
            TelephoneContacts = new HashSet<TelephoneContact>();
        }

        public long ApplicationId { get; set; }
        [DisplayName("Full Name")]
        [Required]
        public string FullName { get; set; }
        [DisplayName("Date Of Birth")]
        public DateTime? DateOfBirth { get; set; }
        [DisplayName("Marital Status")]
        public string MaritalStatus { get; set; }
        [DisplayName("Id Number")]
        [Required]
        public string IdNumber { get; set; }
        [Required]
        public long County { get; set; }
        [DisplayName("Sub County")]
        [Required]
        public long SubCounty { get; set; }
        [Required]
        public long Location { get; set; }
        [DisplayName("Sub Location")]
        [Required]
        public long SubLocation { get; set; }
        [Required]
        public long Village { get; set; }
        [DisplayName("Postal Address")]
        public string PostalAddress { get; set; }
        [DisplayName("Physical Address")]
        [Required]
        public string PhysicalAddress { get; set; }
        [DisplayName("Poor Elderly Persons")]
        public string PoorElderlyPersons { get; set; }
        [DisplayName("Orphan And Vulnerable Children")]
        public string OrphanAndVulnerableChildren { get; set; }
        [DisplayName("Persons With Disability")]
        public string PersonsWithDisability { get; set; }
        [DisplayName("Persons In Extreme Poverty")]
        public string PersonsInExtremePoverty { get; set; }
        [DisplayName("Any Other")]
        public string AnyOther { get; set; }
        [DisplayName("Collector:")]
        [Required]
        public string InfoCollectedBy { get; set; }
        [DisplayName("Designation Of Collector")]
        public string DesignationOfCollector { get; set; }
        [DisplayName("Date Of Collection")]
        [Required]
        public DateTime DateOfCollection { get; set; }
        [DisplayName("Created On")]
        public DateTime? CreatedAt { get; set; }

        [DisplayName("County")]
        public virtual County CountyNavigation { get; set; }
        [DisplayName("Location")]
        public virtual Location LocationNavigation { get; set; }
        [DisplayName("Sub County")]
        public virtual SubCounty SubCountyNavigation { get; set; }
        [DisplayName("Sub Location")]
        public virtual SubLocation SubLocationNavigation { get; set; }
        [DisplayName("Village")]
        public virtual Village VillageNavigation { get; set; }
        [DisplayName("Telephone Contacts")]
        public virtual ICollection<TelephoneContact> TelephoneContacts { get; set; }

    }
}
