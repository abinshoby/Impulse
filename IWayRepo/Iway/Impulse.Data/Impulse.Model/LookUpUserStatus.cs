using System;
using System.Collections.Generic;
using System.Text;

namespace Impulse.Model
{

    public partial class LookUpUserStatus
    {
        public LookUpUserStatus()
        {
            User = new HashSet<User>();
        }

        public long LookUpUserStatusId { get; set; }
        public string UserStatus { get; set; }
        public string RecordStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public ICollection<User> User { get; set; }
    }
}
