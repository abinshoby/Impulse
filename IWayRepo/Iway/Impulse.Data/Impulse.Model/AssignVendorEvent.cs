using System;
using System.Collections.Generic;
using System.Text;

namespace Impulse.Model
{
    public class AssignVendorEvent
    {
        public int EventId { get; set; }
        public int StatusId { get; set; }
        public VIDetails[] VIDetails { get; set; }
    }
    public class VIDetails
    {
        public int EventDetailsId { get; set; }
        public int UserId { get; set; }
    }
}
