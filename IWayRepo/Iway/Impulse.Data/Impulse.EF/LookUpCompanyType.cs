using System;
using System.Collections.Generic;

namespace Impulse.EF
{
    public partial class LookUpCompanyType
    {
        public LookUpCompanyType()
        {
            Company = new HashSet<Company>();
        }

        public long LookUpCompanyTypeId { get; set; }
        public string CompanyType { get; set; }
        public string RecordStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public ICollection<Company> Company { get; set; }
    }
}
