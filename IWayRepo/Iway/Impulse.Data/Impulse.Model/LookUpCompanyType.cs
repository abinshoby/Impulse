using System;
using System.Collections.Generic;
using System.Text;

namespace Impulse.Model
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
       

        public ICollection<Company> Company { get; set; }      

    }
}
