using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable

namespace development_pathways.Models
{
    public partial class TelephoneContact
    {
        public long TelephoneContactId { get; set; }
        [DisplayName("Telephone Contact")]
        public string TelephoneContact1 { get; set; }
        public long ApplicationId { get; set; }
        [DisplayName("Created On")]
        public DateTime? CreatedAt { get; set; }

        public virtual Application Application { get; set; }
    }
}
