using System;
using System.Collections.Generic;

namespace Impulse.EF
{
    public partial class LookUpContactType
    {
        public LookUpContactType()
        {
            Contact = new HashSet<Contact>();
        }

        public long LookUpContactTypeId { get; set; }
        public string ContactType { get; set; }       
        public string RecordStatus { get; set; }
      

        public ICollection<Contact> Contact { get; set; }
    }
}
