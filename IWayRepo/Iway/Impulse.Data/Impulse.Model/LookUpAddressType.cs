//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Impulse.Model
{
    using System;
    using System.Collections.Generic;
    
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
