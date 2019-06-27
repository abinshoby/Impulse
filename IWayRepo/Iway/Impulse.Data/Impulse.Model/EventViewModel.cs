using System;
using System.Collections.Generic;
using System.Text;

namespace Impulse.Model
{
    public class EventViewModel
    {
        public long EventId { get; set; }
        public string Name { get; set; }

        public int? StatusId { get; set; }
        public string StatusName { get; set; }
        public DateTime? ExpectedToConduct { get; set; }
        public string EmployeeRange { get; set; }
        public string VendorName { get; set; }
        public string FinalVendorStatus { get; set; }
        public long? VendorId { get; set; }

        public string CreatedBy { get; set; }
        public long? CorporateId { get; set; }

        public string Location { get; set; }

        public int? ScheduleTypeId { get; set; }
        public string ScheduleType { get; set; }
        
        public DateTime? CreatedOn { get; set; }
        public List<EventDetailsViewModel> EventDetails { get; set; }
        public VendorInvitation VendorInvitation { get; set; }
        public User CreatedUser { get; set; }

        public int UserId { get; set; }
        public int UserTypeId { get; set; }

    }
}
