using System;
using System.Collections.Generic;
using System.Text;

namespace Impulse.EF
{
    public class VendorInterestRate
    {
        public long VendorInterestRateId { get; set; }
        public long VendorInterestId { get; set; }       
        public long? LookupRateTypeId { get; set; }
        public decimal rate { get; set; }
        public string RecordStatus { get; set; }
       
    }
}
