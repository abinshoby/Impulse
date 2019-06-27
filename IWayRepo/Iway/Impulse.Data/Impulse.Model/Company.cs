using System;
using System.Collections.Generic;
using System.Text;

namespace Impulse.Model
{
    public class Company
    {
        public long CompanyId { get; set; }
        public string CompanyName { get; set; }
        public long Pin { get; set; }
        public long UserId { get; set; }
        public long LookUpCompanyTypeId { get; set; }      
        public string RecordStatus { get; set; }      

        public LookUpCompanyType LookUpCompanyType { get; set; }
        public User User { get; set; }
    }
}
