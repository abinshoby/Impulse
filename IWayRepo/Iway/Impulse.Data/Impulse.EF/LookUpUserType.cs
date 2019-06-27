using System;
using System.Collections.Generic;

namespace Impulse.EF
{
    public partial class LookUpUserType
    {
        public LookUpUserType()
        {
            User = new HashSet<User>();
        }

        public long LookUpUserTypeId { get; set; }
        public string UserType { get; set; }
        public string RecordStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public ICollection<User> User { get; set; }
    }
}
