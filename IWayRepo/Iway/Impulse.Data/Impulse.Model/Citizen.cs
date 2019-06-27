using System;
using System.Collections.Generic;
using System.Text;

namespace Impulse.Model
{
    public class Citizen
    {
        public int CitizenId { get; set; }
        public int UserId { get; set; }
        public string CaptainName { get; set; }
        public string PlayerName { get; set; }
        public string CompanyName { get; set; }
        public string Designation { get; set; }
        public DateTime DOB { get; set; }
        public string BloodGroup { get; set; }
        public string HREmail { get; set; }
        public string CompanyEmail { get; set; }
        public string HRPhone { get; set; }
    }
}
