using System;
using System.Collections.Generic;

namespace Impulse.EF
{
    public partial class Contact
    {
        public long ContactId { get; set; }
        public long? LookUpContactTypeId { get; set; }
        public long? UserId { get; set; }
        public string Value { get; set; }
        public string RecordStatus { get; set; }

        public LookUpContactType LookUpContactType { get; set; }
        public User User { get; set; }
    }
}
