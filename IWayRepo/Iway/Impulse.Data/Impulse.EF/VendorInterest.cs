using System;
using System.Collections.Generic;
using System.Text;

namespace Impulse.EF
{
    public class VendorInterest
    {
        public long VendorInterestId { get; set; }
        public long? UserId { get; set; }
        public long? PlaceId { get; set; }
        public string Place { get; set; }
        public long? EventTypeId { get; set; }
        public long? EventSubTypeId { get; set; }
        public string RecordStatus { get; set; }
        public User User { get; set; }
    }
    
}
