using System;
using System.Collections.Generic;

namespace Impulse.EF
{
    public partial class Company
    {
        public long CompanyId { get; set; }
        public string CompanyName { get; set; }
        public long Pin { get; set; }
        public long UserId { get; set; }
        public long LookUpCompanyTypeId { get; set; }
        public string RecordStatus { get; set; }

        public LookUpCompanyType LookUpCompanyType { get; set; }
    }
}
