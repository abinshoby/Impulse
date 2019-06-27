using System;
using System.Collections.Generic;

namespace Impulse.EF
{
    public partial class User
    {
        public User()
        {
            Address = new HashSet<Address>();
            Contact = new HashSet<Contact>();
            Organiser = new HashSet<Organiser>();
            Team = new HashSet<Team>();
        }

        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public long? LookUpUserStatusId { get; set; }
        public long LookUpUserTypeId { get; set; }
        public string RecordStatus { get; set; }

        public LookUpUserStatus LookUpUserStatus { get; set; }
        public LookUpUserType LookUpUserType { get; set; }
        public ICollection<Address> Address { get; set; }
        public ICollection<Contact> Contact { get; set; }
        public ICollection<Organiser> Organiser { get; set; }
        public ICollection<Team> Team { get; set; }
    }
}
