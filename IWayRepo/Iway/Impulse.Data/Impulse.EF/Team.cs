using System;
using System.Collections.Generic;

namespace Impulse.EF
{
    public partial class Team
    {
        public long TeamId { get; set; }
        public string TeamName { get; set; }
        public long? UserId { get; set; }
        public string TeamDescription { get; set; }
        public string TeamLogo { get; set; }
        public long? CompanyId { get; set; }
        public string RecordStatus { get; set; }

        public User User { get; set; }
    }
}
