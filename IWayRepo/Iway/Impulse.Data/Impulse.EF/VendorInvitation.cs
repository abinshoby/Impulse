using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Impulse.EF
{
    public class VendorInvitation
    {
        [Key]
        public long VendorInvitationId { get; set; }
        public long VendorId { get; set; }
        public long EventDetailsId { get; set; }
        public long StatusId { get; set; }
        public string AdminComment { get; set; }
        public string VendorComment { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public string Amount { get; set; }
        public string AdminAmount { get; set; }
        public string AdminPerc { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        
    }
}
