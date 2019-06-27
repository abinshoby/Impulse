using System;
using System.Collections.Generic;

namespace Impulse.EF
{
    public partial class Address
    {
        public long AddressId { get; set; }
        public string Address1 { get; set; }
        public long Pin { get; set; }
        public long UserId { get; set; }
        public long LookUpAddressTypeId { get; set; }
        public string Type { get; set; }
        public string RecordStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public LookUpAddressType LookUpAddressType { get; set; }
        public User User { get; set; }
    }
}
