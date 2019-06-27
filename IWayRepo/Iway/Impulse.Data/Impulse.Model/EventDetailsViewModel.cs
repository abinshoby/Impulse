using System;
using System.Collections.Generic;
using System.Text;

namespace Impulse.Model
{
    public class EventDetailsViewModel
    {
        public Nullable<int> EventTypeId { get; set; }
        public string EventType { get; set; }
        public Nullable<int> EventSubTypeId { get; set; }
        public string EventSubType { get; set; }
        public int SurfaceTypeId { get; set; }
        public string SurfaceType { get; set; }

        public long EventDetailsId { get; set; }
        public long EventId { get; set; }
        public Nullable<int> ScheduleTypeId { get; set; }
        public string Location { get; set; }
        public string EmployeeRange { get; set; }
        public DateTime? ExpectedToConduct { get; set; }
        //public int? AssignedVendorId { get; set; }
        //public string AssignedVendorName { get; set; }

        public List<User> Assignedser { get; set; }
        public List<User> UserListForAssign { get; set; }
        public VendorInvitation VendorInvitation { get; set; }

    }

}
