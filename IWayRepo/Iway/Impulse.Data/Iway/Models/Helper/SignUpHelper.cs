using Impulse.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Iway.Models.Helper
{
    public class SignUpHelper
    {
        public async Task<User> GetUserInput(SignUp _input)
        {
            User _in = new User();
            _in.Team = new Team();
            _in.Contact = new List<Contact>();
            _in.UserName = _input.Email;
            _in.Password = _input.Password;
            _in.LookUpUserTypeId = 1;
            _in.LookUpUserStatusId = 1;
            _in.RecordStatus = "Active";
            Team _inTeam = new Team();
            _inTeam.TeamName = _input.Company;
            _inTeam.TeamDescription = null;
            _inTeam.TeamLogo = null;
            _in.Team = _inTeam;
            List<Contact> _inConL = new List<Contact>();
            Contact _inCon = new Contact();
            _inCon.LookUpContactTypeId = 1;
            _inCon.Value = _input.Email;
            _inConL.Add(_inCon);
            Contact _inConP = new Contact();
            _inConP.LookUpContactTypeId = 2;
            _inConP.Value = _input.Phone;
            _inConL.Add(_inConP);
            _in.Contact = _inConL;
            return await Task.Run(() => _in);
        }

        public async Task<User> GetVendorUserInput(SignUp _input)
        {
            User _in = new User();
            _in.Team = new Team();
            _in.Contact = new List<Contact>();
            _in.UserName = _input.Email;
            _in.Password = _input.Password;
            _in.LookUpUserTypeId = 2;
            _in.LookUpUserStatusId = 4;
            _in.RecordStatus = "Active";
            Team _inTeam = new Team();
            _inTeam.TeamName = _input.Company;
            _inTeam.TeamDescription = null;
            _inTeam.TeamLogo = null;
            _in.Team = _inTeam;
            List<Contact> _inConL = new List<Contact>();
            Contact _inCon = new Contact();
            _inCon.LookUpContactTypeId = 1;
            _inCon.Value = _input.Email;
            _inConL.Add(_inCon);
            Contact _inConP = new Contact();
            _inConP.LookUpContactTypeId = 2;
            _inConP.Value = _input.Phone;
            _inConL.Add(_inConP);
            _in.Contact = _inConL;
            return await Task.Run(() => _in);
        }

        public async Task<User> GetCitizenUserInput(SignUp _input)
        {
            User _in = new User();
            _in.Team = new Team();
            _in.Contact = new List<Contact>();
            _in.UserName = _input.Email;
            _in.Password = _input.Password;
            _in.LookUpUserTypeId = 4;
            _in.LookUpUserStatusId = 1;
            _in.RecordStatus = "Active";
            Team _inTeam = new Team();
            _inTeam.TeamName = _input.Company;
            _inTeam.TeamDescription = null;
            _inTeam.TeamLogo = null;
            _in.Team = _inTeam;
            List<Contact> _inConL = new List<Contact>();
            Contact _inCon = new Contact();
            _inCon.LookUpContactTypeId = 1;
            _inCon.Value = _input.Email;
            _inConL.Add(_inCon);
            Contact _inConP = new Contact();
            _inConP.LookUpContactTypeId = 2;
            _inConP.Value = _input.Phone;
            _inConL.Add(_inConP);
            _in.Contact = _inConL;
            return await Task.Run(() => _in);
        }
    }
}
