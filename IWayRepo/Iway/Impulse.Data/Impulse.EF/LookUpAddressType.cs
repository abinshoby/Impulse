using System;
using System.Collections.Generic;

namespace Impulse.EF
{
    public partial class LookUpAddressType
    {
        public LookUpAddressType()
        {
            Address = new HashSet<Address>();
        }

        public long LookUpAddressTypeId { get; set; }
        public string AddressType { get; set; }
        public string RecordStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public ICollection<Address> Address { get; set; }
    }
}
