using ModelClass = Impulse.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Impulse.Data.BLL.Interface;
using Impulse.Data.BLL.EF;
using Db = Impulse.EF;
using Impulse.Model;
using System.Linq.Expressions;
using System.Transactions;

namespace Impulse.Data.BLL.Handler
{
    public class UserHandler : IUser
    {
        private readonly DataContext _context;
        public UserHandler(DataContext context)
        {
            _context = context;
        }
        public async Task<ModelClass.User> LoginCheck(ModelClass.Login _input)
        {
            try
            {
                ModelClass.User _out = new ModelClass.User();

                var _itemInput = await _context.User.Where(i => i.RecordStatus == "Active" && i.UserName == _input.UserName && i.Password == _input.Password).FirstOrDefaultAsync();

                if (_itemInput != null)
                {
                    _out = await Get(Convert.ToInt32(_itemInput.UserId));
                    return await Task.Run(() => _out);
                }
                else
                {
                    return null;

                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        public async Task<List<ModelClass.User>> Get()
        {
            try
            {
                List<ModelClass.User> _out = new List<ModelClass.User>();

                var _itemInput = await _context.User.Where(m => m.RecordStatus == "Active").ToListAsync();

                if (_itemInput != null)
                {
                    foreach (var itm in _itemInput)
                    {
                        ModelClass.User _inUsr = new ModelClass.User();
                        ModelClass.Company _inCom = new Company();
                        ModelClass.Team _inTeam = new ModelClass.Team();
                        ModelClass.Citizen _citizen = new ModelClass.Citizen();
                        //Details from User table 
                        if (itm != null)
                        {

                            ModelClass.LookUpUserType _inUsrT = new ModelClass.LookUpUserType();
                            ModelClass.LookUpUserStatus _input1 = new ModelClass.LookUpUserStatus();

                            _inUsr.UserId = itm.UserId;
                            _inUsr.UserName = itm.UserName;
                            _inUsr.LookUpUserTypeId = itm.LookUpUserTypeId;
                            _inUsr.LookUpUserStatusId = itm.LookUpUserStatusId;
                            _inUsr.RecordStatus = itm.RecordStatus;
                            _inUsr.VendorInterest = new List<ModelClass.VendorInterest>();
                            if (_inUsr.LookUpUserTypeId == 2)
                            {
                                _inUsr.VendorInterest = await GetVendorInterestbyUser(Convert.ToInt32(_inUsr.UserId));
                            }
                            var _itemInput5 = await _context.LookUpUserType.Where(m => m.LookUpUserTypeId == itm.LookUpUserTypeId && m.RecordStatus == "Active").FirstOrDefaultAsync();
                            if (_itemInput5 != null)
                            {
                                _inUsrT.LookUpUserTypeId = _itemInput5.LookUpUserTypeId;
                                _inUsrT.UserType = _itemInput5.UserType;
                                _inUsrT.RecordStatus = _itemInput5.RecordStatus;
                                _inUsrT.CreatedBy = _itemInput5.CreatedBy;
                                _inUsrT.CreatedDate = _itemInput5.CreatedDate;
                                _inUsrT.UpdatedBy = _itemInput5.UpdatedBy;
                                _inUsrT.UpdatedDate = _itemInput5.UpdatedDate;
                                _inUsr.LookUpUserType = _inUsrT;

                            }

                            var _itemInput6 = await _context.LookUpUserStatus.Where(m => m.LookUpUserStatusId == itm.LookUpUserStatusId && m.RecordStatus == "Active").FirstOrDefaultAsync();
                            if (_itemInput6 != null)
                            {
                                _input1.LookUpUserStatusId = _itemInput6.LookUpUserStatusId;
                                _input1.UserStatus = _itemInput6.UserStatus;
                                _input1.RecordStatus = _itemInput6.RecordStatus;
                                _input1.CreatedBy = _itemInput6.CreatedBy;
                                _input1.CreatedDate = _itemInput6.CreatedDate;
                                _input1.UpdatedBy = _itemInput6.UpdatedBy;
                                _input1.UpdatedDate = _itemInput6.UpdatedDate;
                                _inUsr.LookUpUserStatus = _input1;
                            }


                        }

                        //Details from PersonType table
                        var _itemInput1 = await _context.Team.Where(m => m.RecordStatus == "Active" && m.UserId == itm.UserId).FirstOrDefaultAsync();
                        if (_itemInput1 != null)
                        {
                            _inTeam.TeamId = _itemInput1.TeamId;
                            _inTeam.TeamName = _itemInput1.TeamName;
                            _inTeam.UserId = Convert.ToInt32(_itemInput1.UserId);
                            _inTeam.TeamDescription = _itemInput1.TeamDescription;
                            _inTeam.TeamLogo = _itemInput1.TeamLogo;
                            _inTeam.RecordStatus = _itemInput1.RecordStatus;

                            _inTeam.CompanyId = Convert.ToInt32(_itemInput1.CompanyId);
                            _inUsr.Team = _inTeam;
                        }

                        //Details from LookUpPersonType table
                        var _itemInput2 = await _context.Company.Where(m => m.UserId == itm.UserId && m.RecordStatus == "Active").FirstOrDefaultAsync();
                        if (_itemInput2 != null)
                        {
                            ModelClass.LookUpCompanyType _inLookCom = new ModelClass.LookUpCompanyType();
                            _inCom.CompanyId = _itemInput2.CompanyId;
                            _inCom.CompanyName = _itemInput2.CompanyName;
                            _inCom.Pin = _itemInput2.Pin;
                            _inCom.UserId = _itemInput2.UserId;
                            _inCom.LookUpCompanyTypeId = _itemInput2.LookUpCompanyTypeId;
                            _inCom.RecordStatus = _itemInput2.RecordStatus;
                            var _itemInput6 = await _context.LookUpCompanyType.Where(m => m.LookUpCompanyTypeId == _itemInput2.LookUpCompanyTypeId && m.RecordStatus == "Active").FirstOrDefaultAsync();
                            if (_itemInput6 != null)
                            {
                                _inLookCom.LookUpCompanyTypeId = _itemInput6.LookUpCompanyTypeId;
                                _inLookCom.CompanyType = _itemInput6.CompanyType;
                                _inLookCom.RecordStatus = _itemInput6.RecordStatus;
                                _inCom.LookUpCompanyType = _inLookCom;
                            }
                            _inUsr.Company = _inCom;
                        }



                        //Details from Contact table
                        var _itemInput4 = await _context.Contact.Where(m => m.UserId == itm.UserId && itm.RecordStatus == "Active").ToListAsync();

                        if (_itemInput4 != null && _itemInput4.Count != 0)
                        {
                            List<Contact> listContact = new List<Contact>();
                            foreach (var cnt in _itemInput4)
                            {
                                if (cnt.RecordStatus == "Active")
                                {
                                    ModelClass.Contact _inCnt = new ModelClass.Contact();
                                    ModelClass.LookUpContactType _inCnTp = new ModelClass.LookUpContactType();
                                    _inCnt.ContactId = cnt.ContactId;
                                    _inCnt.Value = cnt.Value;
                                    _inCnt.UserId = Convert.ToInt32(cnt.UserId);
                                    _inCnt.LookUpContactTypeId = Convert.ToInt32(cnt.LookUpContactTypeId);
                                    _inCnt.RecordStatus = cnt.RecordStatus;

                                    var LookUpContactType = await _context.LookUpContactType.Where(m => m.LookUpContactTypeId == cnt.LookUpContactTypeId && cnt.RecordStatus == "Active").FirstOrDefaultAsync();

                                    if (LookUpContactType != null && LookUpContactType.RecordStatus == "Active")
                                    {
                                        _inCnTp.LookUpContactTypeId = LookUpContactType.LookUpContactTypeId;
                                        _inCnTp.ContactType = LookUpContactType.ContactType;
                                        _inCnTp.RecordStatus = LookUpContactType.RecordStatus;

                                        _inCnt.LookUpContactType = _inCnTp;
                                    }
                                    listContact.Add(_inCnt);
                                }
                            }

                            _inUsr.Contact = listContact;
                        }
                        var Citizen = await _context.Citizen.Where(i => i.UserId == itm.UserId).FirstOrDefaultAsync();
                        if (Citizen != null)
                        {

                            _citizen.UserId = Citizen.UserId;
                            _citizen.CitizenId = Citizen.CitizenId;
                            _citizen.CaptainName = Citizen.CaptainName;
                            _citizen.PlayerName = Citizen.PlayerName;
                            _citizen.CompanyName = Citizen.CompanyName;
                            _citizen.Designation = Citizen.Designation;
                            _citizen.DOB = Citizen.DOB;

                            _citizen.BloodGroup = Citizen.BloodGroup;
                            _citizen.HREmail = Citizen.HREmail;
                            _citizen.HRPhone = Citizen.HRPhone;
                            _inUsr.Citizen = _citizen;
                        }
                        
                        _out.Add(_inUsr);
                    }
                    return await Task.Run(() => _out.ToList());
                }
                else
                {

                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<ModelClass.User> Get(int Id)
        {
            try
            {

                ModelClass.User _inUsr = new ModelClass.User();
                ModelClass.Team _inTeam = new ModelClass.Team();
                ModelClass.Citizen _citizen = new ModelClass.Citizen();
                var itm = await _context.User.Where(m => m.RecordStatus == "Active" && m.UserId == Id).FirstOrDefaultAsync();

                if (itm != null)
                {
                    ModelClass.Company _inCom = new Company();

                    //Details from User table 
                    if (itm != null)
                    {

                        ModelClass.LookUpUserType _inUsrT = new ModelClass.LookUpUserType();
                        ModelClass.LookUpUserStatus _input1 = new ModelClass.LookUpUserStatus();

                        _inUsr.UserId = itm.UserId;
                        _inUsr.UserName = itm.UserName;
                        _inUsr.LookUpUserTypeId = itm.LookUpUserTypeId;
                        _inUsr.LookUpUserStatusId = itm.LookUpUserStatusId;
                        _inUsr.RecordStatus = itm.RecordStatus;
                        _inUsr.VendorInterest = new List<ModelClass.VendorInterest>();
                        if (_inUsr.LookUpUserTypeId == 2)
                        {
                            _inUsr.VendorInterest = await GetVendorInterestbyUser(Convert.ToInt32(_inUsr.UserId));
                        }
                        var _itemInput5 = await _context.LookUpUserType.Where(m => m.LookUpUserTypeId == itm.LookUpUserTypeId && m.RecordStatus == "Active").FirstOrDefaultAsync();
                        if (_itemInput5 != null)
                        {
                            _inUsrT.LookUpUserTypeId = _itemInput5.LookUpUserTypeId;
                            _inUsrT.UserType = _itemInput5.UserType;
                            _inUsrT.RecordStatus = _itemInput5.RecordStatus;
                            //_inUsrT.CreatedBy = _itemInput5.CreatedBy;
                            //_inUsrT.CreatedDate = _itemInput5.CreatedDate;
                            //_inUsrT.UpdatedBy = _itemInput5.UpdatedBy;
                            //_inUsrT.UpdatedDate = _itemInput5.UpdatedDate;
                            _inUsr.LookUpUserType = _inUsrT;
                        }

                        var _itemInput6 = await _context.LookUpUserStatus.Where(m => m.LookUpUserStatusId == itm.LookUpUserStatusId && m.RecordStatus == "Active").FirstOrDefaultAsync();
                        if (_itemInput6 != null)
                        {
                            _input1.LookUpUserStatusId = _itemInput6.LookUpUserStatusId;
                            _input1.UserStatus = _itemInput6.UserStatus;
                            _input1.RecordStatus = _itemInput6.RecordStatus;
                            //_input1.CreatedBy = _itemInput6.CreatedBy;
                            //_input1.CreatedDate = _itemInput6.CreatedDate;
                            //_input1.UpdatedBy = _itemInput6.UpdatedBy;
                            //_input1.UpdatedDate = _itemInput6.UpdatedDate;
                            _inUsr.LookUpUserStatus = _input1;
                        }
                    }

                    //Details from PersonType table
                    var _itemInput1 = await _context.Team.Where(m => m.RecordStatus == "Active" && m.UserId == itm.UserId).FirstOrDefaultAsync();
                    if (_itemInput1 != null)
                    {
                        _inTeam.TeamId = _itemInput1.TeamId;
                        _inTeam.TeamName = _itemInput1.TeamName;
                        _inTeam.UserId = Convert.ToInt32(_itemInput1.UserId);
                        _inTeam.TeamDescription = _itemInput1.TeamDescription;
                        _inTeam.TeamLogo = _itemInput1.TeamLogo;
                        _inTeam.RecordStatus = _itemInput1.RecordStatus;
                        _inTeam.CompanyId = Convert.ToInt32(_itemInput1.CompanyId);
                        _inUsr.Team = _inTeam;
                    }

                    //Details from LookUpPersonType table
                    var _itemInput2 = await _context.Company.Where(m => m.UserId == itm.UserId && m.RecordStatus == "Active").FirstOrDefaultAsync();
                    if (_itemInput2 != null)
                    {
                        ModelClass.LookUpCompanyType _inLookCom = new ModelClass.LookUpCompanyType();
                        _inCom.CompanyId = _itemInput2.CompanyId;
                        _inCom.CompanyName = _itemInput2.CompanyName;
                        _inCom.Pin = _itemInput2.Pin;
                        _inCom.UserId = _itemInput2.UserId;
                        _inCom.LookUpCompanyTypeId = _itemInput2.LookUpCompanyTypeId;
                        _inCom.RecordStatus = _itemInput2.RecordStatus;
                        var _itemInput6 = await _context.LookUpCompanyType.Where(m => m.LookUpCompanyTypeId == _itemInput2.LookUpCompanyTypeId && m.RecordStatus == "Active").FirstOrDefaultAsync();
                        if (_itemInput6 != null)
                        {
                            _inLookCom.LookUpCompanyTypeId = _itemInput6.LookUpCompanyTypeId;
                            _inLookCom.CompanyType = _itemInput6.CompanyType;
                            _inLookCom.RecordStatus = _itemInput6.RecordStatus;
                            _inCom.LookUpCompanyType = _inLookCom;
                        }
                        _inUsr.Company = _inCom;
                    }
                    var Citizen = await _context.Citizen.Where(i => i.UserId == itm.UserId).FirstOrDefaultAsync();
                    if (Citizen != null)
                    {

                        _citizen.UserId = Citizen.UserId;
                        _citizen.CitizenId = Citizen.CitizenId;
                        _citizen.CaptainName = Citizen.CaptainName;
                        _citizen.PlayerName = Citizen.PlayerName;
                        _citizen.CompanyName = Citizen.CompanyName;
                        _citizen.Designation = Citizen.Designation;
                        _citizen.DOB = Citizen.DOB;

                        _citizen.BloodGroup = Citizen.BloodGroup;
                        _citizen.HREmail = Citizen.HREmail;
                        _citizen.HRPhone = Citizen.HRPhone;
                        _inUsr.Citizen = _citizen;
                    }

                    //Details from Contact table
                    var _itemInput4 = await _context.Contact.Where(m => m.UserId == itm.UserId && itm.RecordStatus == "Active").ToListAsync();

                    if (_itemInput4 != null && _itemInput4.Count != 0)
                    {
                        List<Contact> listContact = new List<Contact>();
                        foreach (var cnt in _itemInput4)
                        {
                            if (cnt.RecordStatus == "Active")
                            {
                                ModelClass.Contact _inCnt = new ModelClass.Contact();
                                ModelClass.LookUpContactType _inCnTp = new ModelClass.LookUpContactType();
                                _inCnt.ContactId = cnt.ContactId;
                                _inCnt.Value = cnt.Value;
                                _inCnt.UserId = Convert.ToInt32(cnt.UserId);
                                _inCnt.LookUpContactTypeId = Convert.ToInt32(cnt.LookUpContactTypeId);
                                _inCnt.RecordStatus = cnt.RecordStatus;


                                var LookUpContactType = await _context.LookUpContactType.Where(m => m.LookUpContactTypeId == cnt.LookUpContactTypeId && cnt.RecordStatus == "Active").FirstOrDefaultAsync();

                                if (LookUpContactType != null && LookUpContactType.RecordStatus == "Active")
                                {
                                    _inCnTp.LookUpContactTypeId = LookUpContactType.LookUpContactTypeId;
                                    _inCnTp.ContactType = LookUpContactType.ContactType;
                                    _inCnTp.RecordStatus = LookUpContactType.RecordStatus;
                                    _inCnt.LookUpContactType = _inCnTp;
                                }
                                listContact.Add(_inCnt);
                            }
                        }
                        _inUsr.Contact = listContact;
                    }
                    return await Task.Run(() => _inUsr);
                }

                else
                {

                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<List<ModelClass.User>> GetPendingList()
        {
            try
            {
                List<ModelClass.User> _out = new List<ModelClass.User>();

                var _itemInput = await _context.User.Where(m => m.RecordStatus == "Active" && m.LookUpUserStatusId == 2 && m.LookUpUserTypeId == 2).OrderByDescending(x => x.UserId).ToListAsync();

                if (_itemInput != null)
                {
                    foreach (var itm in _itemInput)
                    {
                        ModelClass.User _inUsr = new ModelClass.User();
                        ModelClass.Team _inTeam = new ModelClass.Team();
                        ModelClass.Company _inCom = new Company();

                        //Details from User table 
                        if (itm != null)
                        {

                            ModelClass.LookUpUserType _inUsrT = new ModelClass.LookUpUserType();
                            ModelClass.LookUpUserStatus _input1 = new ModelClass.LookUpUserStatus();

                            _inUsr.UserId = itm.UserId;
                            _inUsr.UserName = itm.UserName;
                            _inUsr.Password = itm.Password;
                            _inUsr.LookUpUserTypeId = itm.LookUpUserTypeId;
                            _inUsr.LookUpUserStatusId = itm.LookUpUserStatusId;
                            _inUsr.RecordStatus = itm.RecordStatus;
                            _inUsr.VendorInterest = new List<ModelClass.VendorInterest>();
                            if (_inUsr.LookUpUserTypeId == 2)
                            {
                                _inUsr.VendorInterest = await GetVendorInterestbyUser(Convert.ToInt32(_inUsr.UserId));
                            }
                            var _itemInput5 = await _context.LookUpUserType.Where(m => m.LookUpUserTypeId == itm.LookUpUserTypeId && m.RecordStatus == "Active").FirstOrDefaultAsync();
                            if (_itemInput5 != null)
                            {
                                _inUsrT.LookUpUserTypeId = _itemInput5.LookUpUserTypeId;
                                _inUsrT.UserType = _itemInput5.UserType;
                                _inUsrT.RecordStatus = _itemInput5.RecordStatus;
                                //_inUsrT.CreatedBy = _itemInput5.CreatedBy;
                                //_inUsrT.CreatedDate = _itemInput5.CreatedDate;
                                //_inUsrT.UpdatedBy = _itemInput5.UpdatedBy;
                                //_inUsrT.UpdatedDate = _itemInput5.UpdatedDate;
                                _inUsr.LookUpUserType = _inUsrT;
                            }

                            var _itemInput6 = await _context.LookUpUserStatus.Where(m => m.LookUpUserStatusId == itm.LookUpUserStatusId && m.RecordStatus == "Active").FirstOrDefaultAsync();
                            if (_itemInput6 != null)
                            {
                                _input1.LookUpUserStatusId = _itemInput6.LookUpUserStatusId;
                                _input1.UserStatus = _itemInput6.UserStatus;
                                _input1.RecordStatus = _itemInput6.RecordStatus;
                                //_input1.CreatedBy = _itemInput6.CreatedBy;
                                //_input1.CreatedDate = _itemInput6.CreatedDate;
                                //_input1.UpdatedBy = _itemInput6.UpdatedBy;
                                //_input1.UpdatedDate = _itemInput6.UpdatedDate;
                                _inUsr.LookUpUserStatus = _input1;
                            }
                        }

                        //Details from PersonType table
                        var _itemInput1 = await _context.Team.Where(m => m.RecordStatus == "Active" && m.UserId == itm.UserId).FirstOrDefaultAsync();
                        if (_itemInput1 != null)
                        {
                            _inTeam.TeamId = _itemInput1.TeamId;
                            _inTeam.TeamName = _itemInput1.TeamName;
                            _inTeam.UserId = Convert.ToInt32(_itemInput1.UserId);
                            _inTeam.TeamDescription = _itemInput1.TeamDescription;
                            _inTeam.TeamLogo = _itemInput1.TeamLogo;
                            _inTeam.RecordStatus = _itemInput1.RecordStatus;

                            _inTeam.CompanyId = Convert.ToInt32(_itemInput1.CompanyId);
                            _inUsr.Team = _inTeam;
                        }

                        //Details from LookUpPersonType table
                        var _itemInput2 = await _context.Company.Where(m => m.UserId == itm.UserId && m.RecordStatus == "Active").FirstOrDefaultAsync();
                        if (_itemInput2 != null)
                        {
                            ModelClass.LookUpCompanyType _inLookCom = new ModelClass.LookUpCompanyType();
                            _inCom.CompanyId = _itemInput2.CompanyId;
                            _inCom.CompanyName = _itemInput2.CompanyName;
                            _inCom.Pin = _itemInput2.Pin;
                            _inCom.UserId = _itemInput2.UserId;
                            _inCom.LookUpCompanyTypeId = _itemInput2.LookUpCompanyTypeId;
                            _inCom.RecordStatus = _itemInput2.RecordStatus;
                            var _itemInput6 = await _context.LookUpCompanyType.Where(m => m.LookUpCompanyTypeId == _itemInput2.LookUpCompanyTypeId && m.RecordStatus == "Active").FirstOrDefaultAsync();
                            if (_itemInput6 != null)
                            {
                                _inLookCom.LookUpCompanyTypeId = _itemInput6.LookUpCompanyTypeId;
                                _inLookCom.CompanyType = _itemInput6.CompanyType;
                                _inLookCom.RecordStatus = _itemInput6.RecordStatus;
                                _inCom.LookUpCompanyType = _inLookCom;
                            }
                            _inUsr.Company = _inCom;
                        }



                        //Details from Contact table
                        var _itemInput4 = await _context.Contact.Where(m => m.UserId == itm.UserId && itm.RecordStatus == "Active").ToListAsync();

                        if (_itemInput4 != null && _itemInput4.Count != 0)
                        {
                            List<Contact> listContact = new List<Contact>();
                            foreach (var cnt in _itemInput4)
                            {
                                if (cnt.RecordStatus == "Active")
                                {
                                    ModelClass.Contact _inCnt = new ModelClass.Contact();
                                    ModelClass.LookUpContactType _inCnTp = new ModelClass.LookUpContactType();
                                    _inCnt.ContactId = cnt.ContactId;
                                    _inCnt.Value = cnt.Value;
                                    _inCnt.UserId = Convert.ToInt32(cnt.UserId);
                                    _inCnt.LookUpContactTypeId = Convert.ToInt32(cnt.LookUpContactTypeId);
                                    _inCnt.RecordStatus = cnt.RecordStatus;

                                    var LookUpContactType = await _context.LookUpContactType.Where(m => m.LookUpContactTypeId == cnt.LookUpContactTypeId && cnt.RecordStatus == "Active").FirstOrDefaultAsync();

                                    if (LookUpContactType != null && LookUpContactType.RecordStatus == "Active")
                                    {
                                        _inCnTp.LookUpContactTypeId = LookUpContactType.LookUpContactTypeId;
                                        _inCnTp.ContactType = LookUpContactType.ContactType;
                                        _inCnTp.RecordStatus = LookUpContactType.RecordStatus;
                                        //_inCnTp.CreatedBy = LookUpContactType.CreatedBy;
                                        //_inCnTp.CreatedDate = LookUpContactType.CreatedDate;
                                        //_inCnTp.UpdatedBy = LookUpContactType.UpdatedBy;
                                        //_inCnTp.UpdatedDate = LookUpContactType.UpdatedDate;
                                        _inCnt.LookUpContactType = _inCnTp;
                                    }
                                    listContact.Add(_inCnt);

                                }
                            }

                            _inUsr.Contact = listContact;
                        }
                        _out.Add(_inUsr);
                    }
                    return await Task.Run(() => _out.ToList());
                }
                else
                {

                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<List<ModelClass.User>> GetUserListByType(int UserTypeId)
        {
            try
            {
                List<ModelClass.User> _out = new List<ModelClass.User>();

                var _itemInput = await _context.User.Where(m => m.RecordStatus == "Active" && m.LookUpUserTypeId == UserTypeId && m.LookUpUserStatusId != 5).OrderByDescending(x => x.UserId).ToListAsync();

                if (_itemInput != null)
                {
                    foreach (var itm in _itemInput)
                    {
                        ModelClass.User _inUsr = new ModelClass.User();
                        ModelClass.Team _inTeam = new ModelClass.Team();
                        ModelClass.Company _inCom = new Company();

                        //Details from User table 
                        if (itm != null)
                        {

                            ModelClass.LookUpUserType _inUsrT = new ModelClass.LookUpUserType();
                            ModelClass.LookUpUserStatus _input1 = new ModelClass.LookUpUserStatus();

                            _inUsr.UserId = itm.UserId;
                            _inUsr.UserName = itm.UserName;
                            _inUsr.Password = itm.Password;
                            _inUsr.LookUpUserTypeId = itm.LookUpUserTypeId;
                            _inUsr.LookUpUserStatusId = itm.LookUpUserStatusId;
                            _inUsr.RecordStatus = itm.RecordStatus;
                            if (_inUsr.LookUpUserTypeId == 2)
                            {
                                _inUsr.VendorInterest = await GetVendorInterestbyUser(Convert.ToInt32(_inUsr.UserId));
                            }
                            var _itemInput5 = await _context.LookUpUserType.Where(m => m.LookUpUserTypeId == itm.LookUpUserTypeId && m.RecordStatus == "Active").FirstOrDefaultAsync();
                            if (_itemInput5 != null)
                            {
                                _inUsrT.LookUpUserTypeId = _itemInput5.LookUpUserTypeId;
                                _inUsrT.UserType = _itemInput5.UserType;
                                _inUsrT.RecordStatus = _itemInput5.RecordStatus;
                                //_inUsrT.CreatedBy = _itemInput5.CreatedBy;
                                //_inUsrT.CreatedDate = _itemInput5.CreatedDate;
                                //_inUsrT.UpdatedBy = _itemInput5.UpdatedBy;
                                //_inUsrT.UpdatedDate = _itemInput5.UpdatedDate;
                                _inUsr.LookUpUserType = _inUsrT;
                            }

                            var _itemInput6 = await _context.LookUpUserStatus.Where(m => m.LookUpUserStatusId == itm.LookUpUserStatusId && m.RecordStatus == "Active").FirstOrDefaultAsync();
                            if (_itemInput6 != null)
                            {
                                _input1.LookUpUserStatusId = _itemInput6.LookUpUserStatusId;
                                _input1.UserStatus = _itemInput6.UserStatus;
                                _input1.RecordStatus = _itemInput6.RecordStatus;
                                //_input1.CreatedBy = _itemInput6.CreatedBy;
                                //_input1.CreatedDate = _itemInput6.CreatedDate;
                                //_input1.UpdatedBy = _itemInput6.UpdatedBy;
                                //_input1.UpdatedDate = _itemInput6.UpdatedDate;
                                _inUsr.LookUpUserStatus = _input1;
                            }
                        }

                        //Details from PersonType table
                        var _itemInput1 = await _context.Team.Where(m => m.RecordStatus == "Active" && m.UserId == itm.UserId).FirstOrDefaultAsync();
                        if (_itemInput1 != null)
                        {
                            _inTeam.TeamId = _itemInput1.TeamId;
                            _inTeam.TeamName = _itemInput1.TeamName;
                            _inTeam.UserId = Convert.ToInt32(_itemInput1.UserId);
                            _inTeam.TeamDescription = _itemInput1.TeamDescription;
                            _inTeam.TeamLogo = _itemInput1.TeamLogo;
                            _inTeam.RecordStatus = _itemInput1.RecordStatus;

                            _inTeam.CompanyId = Convert.ToInt32(_itemInput1.CompanyId);
                            _inUsr.Team = _inTeam;
                        }

                        //Details from LookUpPersonType table
                        var _itemInput2 = await _context.Company.Where(m => m.UserId == itm.UserId && m.RecordStatus == "Active").FirstOrDefaultAsync();
                        if (_itemInput2 != null)
                        {
                            ModelClass.LookUpCompanyType _inLookCom = new ModelClass.LookUpCompanyType();
                            _inCom.CompanyId = _itemInput2.CompanyId;
                            _inCom.CompanyName = _itemInput2.CompanyName;
                            _inCom.Pin = _itemInput2.Pin;
                            _inCom.UserId = _itemInput2.UserId;
                            _inCom.LookUpCompanyTypeId = _itemInput2.LookUpCompanyTypeId;
                            _inCom.RecordStatus = _itemInput2.RecordStatus;
                            var _itemInput6 = await _context.LookUpCompanyType.Where(m => m.LookUpCompanyTypeId == _itemInput2.LookUpCompanyTypeId && m.RecordStatus == "Active").FirstOrDefaultAsync();
                            if (_itemInput6 != null)
                            {
                                _inLookCom.LookUpCompanyTypeId = _itemInput6.LookUpCompanyTypeId;
                                _inLookCom.CompanyType = _itemInput6.CompanyType;
                                _inLookCom.RecordStatus = _itemInput6.RecordStatus;
                                _inCom.LookUpCompanyType = _inLookCom;
                            }
                            _inUsr.Company = _inCom;
                        }



                        //Details from Contact table
                        var _itemInput4 = await _context.Contact.Where(m => m.UserId == itm.UserId && itm.RecordStatus == "Active").ToListAsync();

                        if (_itemInput4 != null && _itemInput4.Count != 0)
                        {
                            List<Contact> listContact = new List<Contact>();
                            foreach (var cnt in _itemInput4)
                            {
                                if (cnt.RecordStatus == "Active")
                                {
                                    ModelClass.Contact _inCnt = new ModelClass.Contact();
                                    ModelClass.LookUpContactType _inCnTp = new ModelClass.LookUpContactType();
                                    _inCnt.ContactId = cnt.ContactId;
                                    _inCnt.Value = cnt.Value;
                                    _inCnt.UserId = Convert.ToInt32(cnt.UserId);
                                    _inCnt.LookUpContactTypeId = Convert.ToInt32(cnt.LookUpContactTypeId);
                                    _inCnt.RecordStatus = cnt.RecordStatus;

                                    var LookUpContactType = await _context.LookUpContactType.Where(m => m.LookUpContactTypeId == cnt.LookUpContactTypeId && cnt.RecordStatus == "Active").FirstOrDefaultAsync();

                                    if (LookUpContactType != null && LookUpContactType.RecordStatus == "Active")
                                    {
                                        _inCnTp.LookUpContactTypeId = LookUpContactType.LookUpContactTypeId;
                                        _inCnTp.ContactType = LookUpContactType.ContactType;
                                        _inCnTp.RecordStatus = LookUpContactType.RecordStatus;
                                        //_inCnTp.CreatedBy = LookUpContactType.CreatedBy;
                                        //_inCnTp.CreatedDate = LookUpContactType.CreatedDate;
                                        //_inCnTp.UpdatedBy = LookUpContactType.UpdatedBy;
                                        //_inCnTp.UpdatedDate = LookUpContactType.UpdatedDate;
                                        _inCnt.LookUpContactType = _inCnTp;
                                    }
                                    listContact.Add(_inCnt);

                                }
                            }
                            _inUsr.Contact = listContact;
                        }
                        _out.Add(_inUsr);
                    }
                    return await Task.Run(() => _out.ToList());
                }
                else
                {

                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<ModelClass.User> Post(ModelClass.User input)
        {
            try
            {
                ModelClass.User _out = new ModelClass.User();
                var _itemInput = await _context.User.Where(i => i.RecordStatus == "Active" && i.UserName == input.UserName).FirstOrDefaultAsync();
                if (_itemInput == null)
                {


                    Db.User _inUsr = new Db.User();
                    _out.Status = 0;
                    _inUsr.UserName = input.UserName;
                    _inUsr.Password = input.Password;
                    _inUsr.LookUpUserTypeId = input.LookUpUserTypeId;
                    _inUsr.LookUpUserStatusId = input.LookUpUserStatusId;
                    _inUsr.RecordStatus = "Active";
                    _context.User.Add(_inUsr);
                    _context.SaveChanges();

                    Db.Team _itemIn = new Db.Team();
                    _itemIn.TeamName = input.Team.TeamName;
                    _itemIn.UserId = _inUsr.UserId;
                    _itemIn.TeamDescription = input.Team.TeamDescription;
                    _itemIn.TeamLogo = input.Team.TeamLogo;
                    _itemIn.RecordStatus = "Active";
                    _context.Team.Add(_itemIn);
                    _context.SaveChanges();

                    //if (input.Contact.Count != 0)
                    foreach (var itemSub in input.Contact)
                    {
                        Db.Contact _itemSub = new Db.Contact();
                        _itemSub.ContactId = itemSub.ContactId;
                        _itemSub.Value = itemSub.Value;
                        _itemSub.LookUpContactTypeId = itemSub.LookUpContactTypeId;
                        _itemSub.UserId = _inUsr.UserId;
                        _itemSub.RecordStatus = "Active";
                        _context.Contact.Add(_itemSub);
                        _context.SaveChanges();

                    }
                    _out.UserId = _inUsr.UserId;
                    _out.UserName = _inUsr.UserName;
                    _out.UserId = _inUsr.UserId;
                    _out.LookUpUserTypeId = _inUsr.LookUpUserTypeId;
                    _out.LookUpUserType = new LookUpUserType()
                    {
                        LookUpUserTypeId= _inUsr.LookUpUserTypeId,
                        UserType= _context.LookUpUserType.Where(i => i.LookUpUserTypeId == _inUsr.LookUpUserTypeId).FirstOrDefault().UserType
                    };
                   
                }
                else
                {
                    _out.Status = 1;
                }
                return await Task.Run(() => _out);



            }
            catch (Exception)
            {

                throw;

            }

        }
        public async Task<ModelClass.User> Put(ModelClass.User input)
        {
            try
            {
                ModelClass.User _out = new ModelClass.User();

                var _inUsr = await _context.User.Where(i => i.UserId == input.UserId && i.RecordStatus == "Active").FirstOrDefaultAsync();
                _inUsr.UserName = input.UserName;
                _inUsr.Password = input.Password;
                _inUsr.LookUpUserTypeId = input.LookUpUserTypeId;
                _inUsr.LookUpUserStatusId = input.LookUpUserStatusId;
                _context.SaveChanges();

                var _itemIn = await _context.Team.Where(i => i.UserId == input.UserId && i.RecordStatus == "Active").FirstOrDefaultAsync();
                _itemIn.TeamName = input.Team.TeamName;
                _itemIn.TeamDescription = input.Team.TeamDescription;
                _itemIn.TeamLogo = input.Team.TeamLogo;
                _context.SaveChanges();

                var _itemSubList = await _context.Contact.Where(i => i.UserId == input.UserId && i.RecordStatus == "Active").ToListAsync();
                //if (input.Contact.Count != 0)
                if (_itemSubList != null)
                {
                    foreach (var itemSub in _itemSubList)
                    {
                        var _itemSub = await _context.Contact.Where(i => i.ContactId == itemSub.ContactId && i.RecordStatus == "Active").FirstOrDefaultAsync();
                        if (_itemSub != null)
                        {
                            _itemSub.ContactId = itemSub.ContactId;
                            _itemSub.Value = itemSub.Value;
                            _itemSub.LookUpContactTypeId = itemSub.LookUpContactTypeId;
                            _context.SaveChanges();
                        }
                        else
                        {
                            Db.Contact _itemSub1 = new Db.Contact();
                            _itemSub1.Value = itemSub.Value;
                            _itemSub1.LookUpContactTypeId = itemSub.LookUpContactTypeId;
                            _itemSub1.UserId = _inUsr.UserId;
                            _itemSub1.RecordStatus = "Active";
                            _context.Contact.Add(_itemSub);
                            _context.SaveChanges();
                        }
                    }
                }
                _out.UserName = _inUsr.UserName;
                _out.Password = _inUsr.Password;
                _out.LookUpUserTypeId = _inUsr.LookUpUserTypeId;
                _out.LookUpUserStatusId = _inUsr.LookUpUserStatusId;
                return await Task.Run(() => _out);



            }
            catch (Exception)
            {

                throw;

            }

        }
        public async Task<User> PostCompanyDetails(ModelClass.User user)
        {
            User _out = new User();

            try
            {
                var _itemInput1 = await _context.Company.Where(m => m.UserId == user.UserId && m.RecordStatus == "Active").FirstOrDefaultAsync();
                if (_itemInput1 != null)
                {
                    _itemInput1.CompanyName = user.Company.CompanyName;
                    _itemInput1.Pin = user.Company.Pin;
                    _itemInput1.LookUpCompanyTypeId = user.Company.LookUpCompanyTypeId;
                    _context.SaveChanges();

                }
                else
                {
                    Db.Company _itemSub1 = new Db.Company();
                    _itemSub1.CompanyName = user.Company.CompanyName;
                    _itemSub1.Pin = user.Company.Pin;
                    _itemSub1.LookUpCompanyTypeId = user.Company.LookUpCompanyTypeId;
                    _itemSub1.UserId = user.UserId;
                    _itemSub1.RecordStatus = "Active";
                    _context.Company.Add(_itemSub1);
                    _context.SaveChanges();
                }

                return await Task.Run(() => _out);
            }
            catch (Exception)
            {


                throw;
            }
        }
        public async Task<User> PutPendingUser(ModelClass.User user)
        {
            User _out = new User();

            try
            {
                var _itemInput1 = await _context.User.Where(m => m.UserId == user.UserId && m.RecordStatus == "Active").FirstOrDefaultAsync();

                if (_itemInput1 != null)
                {
                    _itemInput1.LookUpUserStatusId = user.LookUpUserStatusId;
                    _context.SaveChanges();
                    _out.UserId = _itemInput1.UserId;
                }

                return await Task.Run(() => _out);
            }
            catch (Exception)
            {


                throw;
            }
        }
        public async Task<bool> Delete(int Id)
        {
            try
            {
                ModelClass.Team _out = new ModelClass.Team();

                var _inUsr = await _context.User.Where(i => i.UserId == Id && i.RecordStatus == "Active").FirstOrDefaultAsync();
                _inUsr.RecordStatus = "InActve";
                _context.SaveChanges();

                var _itemIn = await _context.Team.Where(i => i.UserId == _inUsr.UserId && i.RecordStatus == "Active").FirstOrDefaultAsync();
                _itemIn.RecordStatus = "InActve";
                _context.SaveChanges();

                var _itemSubList = await _context.Contact.Where(i => i.UserId == _inUsr.UserId && i.RecordStatus == "Active").ToListAsync();
                //if (input.Contact.Count != 0)
                if (_itemSubList != null)
                {
                    foreach (var itemSub in _itemSubList)
                    {
                        var _itemSub = await _context.Contact.Where(i => i.ContactId == itemSub.ContactId && i.RecordStatus == "Active").FirstOrDefaultAsync();
                        if (_itemSub != null)
                        {
                            _itemSub.RecordStatus = "InActve";
                            _context.SaveChanges();
                        }
                    }
                }

                return await Task.Run(() => true);



            }
            catch (Exception)
            {

                throw;

            }

        }
        public async Task<bool> PostVendorInterest(List<ModelClass.VendorInterest> input)
        {
            try
            {
                foreach (var item in input)
                {
                    ModelClass.VendorInterest _out = new ModelClass.VendorInterest();
                    Db.VendorInterest _itemInput = await _context.VendorInterest.Where(i => i.RecordStatus == "Active" && i.UserId == item.UserId && i.EventTypeId == item.EventTypeId && i.EventSubTypeId == item.EventSubTypeId).FirstOrDefaultAsync();
                    if (_itemInput == null)
                    {
                        Db.VendorInterest _inUsr = new Db.VendorInterest();
                        _inUsr.UserId = item.UserId;
                        _inUsr.PlaceId = item.PlaceId;
                        _inUsr.Place = item.Place;
                        _inUsr.EventTypeId = item.EventTypeId;
                        _inUsr.EventSubTypeId = item.EventSubTypeId;
                        _inUsr.RecordStatus = "Active";
                        _context.VendorInterest.Add(_inUsr);
                        _context.SaveChanges();

                        //if (input.Contact.Count != 0)
                        if (item.VendorInterestRate != null)
                        {
                            if (item.VendorInterestRate.Count() > 0)
                            {
                                foreach (var itemSub in item.VendorInterestRate)
                                {
                                    if (itemSub != null)
                                    {
                                        Db.VendorInterestRate _itemSub = new Db.VendorInterestRate();
                                        _itemSub.VendorInterestId = _inUsr.VendorInterestId;
                                        _itemSub.LookupRateTypeId = itemSub.LookupRateTypeId;
                                        _itemSub.rate = itemSub.rate;
                                        _itemSub.RecordStatus = "Active";
                                        _context.VendorInterestRate.Add(_itemSub);
                                        _context.SaveChanges();
                                    }
                                }
                            }
                        }
                        _out.UserId = _inUsr.UserId;
                    }
                    else
                    {

                        _itemInput.Place = item.Place;
                        _itemInput.RecordStatus = "Active";
                        _context.SaveChanges();
                    }


                }
                return await Task.Run(() => true);
            }
            catch (Exception)
            {

                throw;

            }

        }
        public async Task<bool> UpdateVendorInterest(List<ModelClass.VendorInterest> input)
        {
            try
            {
                foreach (var item in input)
                {

                    Db.VendorInterest _inUsr = await _context.VendorInterest.Where(m => m.VendorInterestId == item.VendorInterestId).FirstOrDefaultAsync();
                    _inUsr.PlaceId = item.PlaceId;
                    _inUsr.Place = item.Place;
                    _inUsr.EventTypeId = item.EventTypeId;
                    _inUsr.EventSubTypeId = item.EventSubTypeId;
                    _context.SaveChanges();
                    //if (input.Contact.Count != 0)
                    foreach (var itemSub in item.VendorInterestRate)
                    {
                        Db.VendorInterestRate _itemSub = await _context.VendorInterestRate.Where(m => m.VendorInterestRateId == itemSub.VendorInterestRateId).FirstOrDefaultAsync();
                        _itemSub.LookupRateTypeId = itemSub.LookupRateTypeId;
                        _itemSub.rate = itemSub.rate;
                        _context.SaveChanges();

                    }
                    return await Task.Run(() => true);
                }
                return await Task.Run(() => false);
            }
            catch (Exception)
            {

                throw;

            }

        }
        public async Task<List<ModelClass.VendorInterest>> GetVendorInterestbyUser(int userId)
        {
            try
            {

                List<ModelClass.VendorInterest> _out = new List<ModelClass.VendorInterest>();
                List<Db.VendorInterest> _itemInput = new List<Db.VendorInterest>();
                _itemInput = await _context.VendorInterest.Where(m => m.RecordStatus == "Active" && m.UserId == userId).ToListAsync();


                if (_itemInput != null)
                {
                    foreach (var itm in _itemInput)
                    {

                        ModelClass.VendorInterest _inUsr = new ModelClass.VendorInterest();
                        ModelClass.EventType _inEve = new ModelClass.EventType();
                        ModelClass.EventSubType _inCom = new EventSubType();
                        List<ModelClass.VendorInterestRate> _inTeam = new List<ModelClass.VendorInterestRate>();
                        //Details from User table 
                        if (itm != null)
                        {

                            ModelClass.LookUpRateType _inR = new ModelClass.LookUpRateType();

                            _inUsr.VendorInterestId = itm.VendorInterestId;
                            _inUsr.PlaceId = itm.PlaceId;
                            _inUsr.UserId = itm.UserId;
                            _inUsr.Place = itm.Place;
                            _inUsr.EventTypeId = itm.EventTypeId;
                            _inUsr.EventSubTypeId = itm.EventSubTypeId;
                            _inUsr.RecordStatus = itm.RecordStatus;

                            var _itemInput5 = await _context.EventType.Where(m => m.EventTypeId == itm.EventTypeId).FirstOrDefaultAsync();
                            if (_itemInput5 != null)
                            {
                                _inEve.EventTypeId = _itemInput5.EventTypeId;
                                _inEve.EventTypeName = _itemInput5.EventTypeName;
                                _inUsr.EventType = new EventType();
                                _inUsr.EventType = _inEve;
                            }
                            var _itemInput6 = await _context.EventSubType.Where(m => m.EventSubTypeId == itm.EventSubTypeId).FirstOrDefaultAsync();
                            if (_itemInput6 != null)
                            {
                                _inCom.EventSubTypeId = _itemInput6.EventSubTypeId;
                                _inCom.SubTypeName = _itemInput6.SubTypeName;
                                _inCom.EventTypeId = _itemInput6.EventTypeId;
                                _inUsr.EventSubType = _inCom;
                            }
                        }

                        //Details from PersonType table
                        var _itemInput1 = await _context.VendorInterestRate.Where(m => m.RecordStatus == "Active" && m.VendorInterestId == itm.VendorInterestId).FirstOrDefaultAsync();
                        if (_itemInput1 != null)
                        {
                            ModelClass.VendorInterestRate _inRate = new ModelClass.VendorInterestRate();
                            _inRate.VendorInterestRateId = _itemInput1.VendorInterestRateId;
                            _inRate.rate = _itemInput1.rate;
                            _inRate.LookupRateTypeId = Convert.ToInt32(_itemInput1.LookupRateTypeId);
                            _inRate.RecordStatus = _itemInput1.RecordStatus;
                            var _itemInput7 = await _context.LookUpRateType.Where(m => m.LookUpRateTypeId == _itemInput1.LookupRateTypeId && m.RecordStatus == "Active").FirstOrDefaultAsync();
                            if (_itemInput7 != null)
                            {
                                ModelClass.LookUpRateType _inLookCom = new LookUpRateType();
                                _inLookCom.LookUpRateTypeId = _itemInput7.LookUpRateTypeId;
                                _inLookCom.RateType = _itemInput7.RateType;
                                _inLookCom.RecordStatus = _itemInput7.RecordStatus;
                                _inRate.LookUpRateType = _inLookCom;
                            }
                            _inTeam.Add(_inRate);
                        }
                        _inUsr.VendorInterestRate = _inTeam;
                        _out.Add(_inUsr);

                    }

                    return await Task.Run(() => _out.ToList());
                }
                else
                {

                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<ModelClass.VendorInterest> GetVendorInterestDetails(int VIId)
        {
            try
            {

                ModelClass.VendorInterest _out = new ModelClass.VendorInterest();
                var itm = await _context.VendorInterest.Where(m => m.RecordStatus == "Active" && m.VendorInterestId == VIId).FirstOrDefaultAsync();

                if (itm != null)
                {
                    ModelClass.VendorInterest _inUsr = new ModelClass.VendorInterest();
                    ModelClass.EventType _inEve = new ModelClass.EventType();
                    ModelClass.EventSubType _inCom = new EventSubType();
                    List<ModelClass.VendorInterestRate> _inTeam = new List<ModelClass.VendorInterestRate>();
                    //Details from User table 
                    if (itm != null)
                    {

                        ModelClass.LookUpRateType _inR = new ModelClass.LookUpRateType();

                        _inUsr.VendorInterestId = itm.VendorInterestId;
                        _inUsr.PlaceId = itm.PlaceId;
                        _inUsr.UserId = itm.UserId;
                        _inUsr.Place = itm.Place;
                        _inUsr.EventTypeId = itm.EventTypeId;
                        _inUsr.EventSubTypeId = itm.EventSubTypeId;
                        _inUsr.RecordStatus = itm.RecordStatus;

                        var _itemInput5 = await _context.EventType.Where(m => m.EventTypeId == itm.EventTypeId).FirstOrDefaultAsync();
                        if (_itemInput5 != null)
                        {
                            _inEve.EventTypeId = _itemInput5.EventTypeId;
                            _inEve.EventTypeName = _itemInput5.EventTypeName;
                            _inUsr.EventType = new EventType();
                            _inUsr.EventType = _inEve;
                        }
                        var _itemInput6 = await _context.EventSubType.Where(m => m.EventSubTypeId == itm.EventSubTypeId).FirstOrDefaultAsync();
                        if (_itemInput6 != null)
                        {
                            _inCom.EventSubTypeId = _itemInput6.EventSubTypeId;
                            _inCom.SubTypeName = _itemInput6.SubTypeName;
                            _inCom.EventTypeId = _itemInput6.EventTypeId;
                            _inUsr.EventSubType = _inCom;
                        }
                    }
                    //Details from PersonType table
                    var _itemInput1 = await _context.VendorInterestRate.Where(m => m.RecordStatus == "Active" && m.VendorInterestId == itm.VendorInterestId).FirstOrDefaultAsync();
                    if (_itemInput1 != null)
                    {
                        ModelClass.VendorInterestRate _inRate = new ModelClass.VendorInterestRate();
                        _inRate.VendorInterestRateId = _itemInput1.VendorInterestRateId;
                        _inRate.rate = _itemInput1.rate;
                        _inRate.LookupRateTypeId = Convert.ToInt32(_itemInput1.LookupRateTypeId);
                        _inRate.RecordStatus = _itemInput1.RecordStatus;
                        var _itemInput7 = await _context.LookUpRateType.Where(m => m.LookUpRateTypeId == _itemInput1.LookupRateTypeId && m.RecordStatus == "Active").FirstOrDefaultAsync();
                        if (_itemInput7 != null)
                        {
                            ModelClass.LookUpRateType _inLookCom = new LookUpRateType();
                            _inLookCom.LookUpRateTypeId = _itemInput7.LookUpRateTypeId;
                            _inLookCom.RateType = _itemInput7.RateType;
                            _inLookCom.RecordStatus = _itemInput7.RecordStatus;
                            _inRate.LookUpRateType = _inLookCom;
                        }
                        _inTeam.Add(_inRate);
                    }
                    _inUsr.VendorInterestRate = _inTeam;

                    return await Task.Run(() => _inUsr);
                }
                else
                {

                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<List<ModelClass.User>> GetVendorInterest(int eventTId, int eventSTId, int EvenDetailsID)
        {
            try
            {
                List<ModelClass.User> _outUser = new List<ModelClass.User>();

                List<ModelClass.VendorInterest> _out = new List<ModelClass.VendorInterest>();
                List<Db.VendorInterest> _itemInput = new List<Db.VendorInterest>();
                if ((eventSTId != 0) && (eventTId != 0))
                {
                    _itemInput = await _context.VendorInterest.Where(m => m.RecordStatus == "Active" && m.EventTypeId == eventTId && m.EventSubTypeId == eventSTId).ToListAsync();
                }
                else if ((eventSTId != 0) && (eventTId == 0))
                {
                    _itemInput = await _context.VendorInterest.Where(m => m.RecordStatus == "Active" && m.EventSubTypeId == eventSTId).ToListAsync();
                }
                else if ((eventSTId == 0) && (eventTId != 0))
                {
                    _itemInput = await _context.VendorInterest.Where(m => m.RecordStatus == "Active" && m.EventTypeId == eventTId).ToListAsync();
                }

                else
                {
                    _itemInput = null;
                }

                if (_itemInput != null)
                {
                    foreach (var itm in _itemInput)
                    {
                        ModelClass.User userdta = new ModelClass.User();
                        ModelClass.VendorInterest _inUsr = new ModelClass.VendorInterest();
                        ModelClass.EventType _inEve = new ModelClass.EventType();
                        ModelClass.EventSubType _inCom = new EventSubType();
                        ModelClass.VendorInvitation VendorInvitation = new VendorInvitation();
                        List<ModelClass.VendorInterestRate> _inTeam = new List<ModelClass.VendorInterestRate>();
                        //Details from User table 
                        if (itm != null)
                        {
                            userdta = await Get(Convert.ToInt32(itm.UserId));

                            ModelClass.LookUpRateType _inR = new ModelClass.LookUpRateType();
                            if (userdta.LookUpUserStatusId == 1)
                            {
                                _inUsr.VendorInterestId = itm.VendorInterestId;
                                _inUsr.PlaceId = itm.PlaceId;
                                _inUsr.UserId = itm.UserId;
                                _inUsr.Place = itm.Place;
                                _inUsr.EventTypeId = itm.EventTypeId;
                                _inUsr.EventSubTypeId = itm.EventSubTypeId;
                                _inUsr.RecordStatus = itm.RecordStatus;

                                var _itemInput5 = await _context.EventType.Where(m => m.EventTypeId == itm.EventTypeId).FirstOrDefaultAsync();
                                if (_itemInput5 != null)
                                {
                                    _inEve.EventTypeId = _itemInput5.EventTypeId;
                                    _inEve.EventTypeName = _itemInput5.EventTypeName;
                                    _inUsr.EventType = new EventType();
                                    _inUsr.EventType = _inEve;
                                }
                                var _itemInput6 = await _context.EventSubType.Where(m => m.EventSubTypeId == itm.EventSubTypeId).FirstOrDefaultAsync();
                                if (_itemInput6 != null)
                                {
                                    _inCom.EventSubTypeId = _itemInput6.EventSubTypeId;
                                    _inCom.SubTypeName = _itemInput6.SubTypeName;
                                    _inCom.EventTypeId = _itemInput6.EventTypeId;
                                    _inUsr.EventSubType = _inCom;
                                }


                                //Details from PersonType table
                                var _itemInput1 = await _context.VendorInterestRate.Where(m => m.RecordStatus == "Active" && m.VendorInterestId == itm.VendorInterestId).FirstOrDefaultAsync();
                                if (_itemInput1 != null)
                                {
                                    ModelClass.VendorInterestRate _inRate = new ModelClass.VendorInterestRate();
                                    _inRate.VendorInterestRateId = _itemInput1.VendorInterestRateId;
                                    _inRate.rate = _itemInput1.rate;
                                    _inRate.LookupRateTypeId = Convert.ToInt32(_itemInput1.LookupRateTypeId);
                                    _inRate.RecordStatus = _itemInput1.RecordStatus;
                                    var _itemInput7 = await _context.LookUpRateType.Where(m => m.LookUpRateTypeId == _itemInput1.LookupRateTypeId && m.RecordStatus == "Active").FirstOrDefaultAsync();
                                    if (_itemInput7 != null)
                                    {
                                        ModelClass.LookUpRateType _inLookCom = new LookUpRateType();
                                        _inLookCom.LookUpRateTypeId = _itemInput7.LookUpRateTypeId;
                                        _inLookCom.RateType = _itemInput7.RateType;
                                        _inLookCom.RecordStatus = _itemInput7.RecordStatus;
                                        _inRate.LookUpRateType = _inLookCom;
                                    }
                                    _inTeam.Add(_inRate);
                                }
                                _inUsr.VendorInterestRate = _inTeam;
                                _out.Add(_inUsr);
                                userdta.VendorInterest = _out;
                                _outUser.Add(userdta);
                                var vI = await _context.VendorInvitation.Where(m => m.EventDetailsId == EvenDetailsID && m.VendorId == itm.UserId).FirstOrDefaultAsync();
                                if (vI != null)
                                {
                                    VendorInvitation.VendorInvitationId = vI.VendorInvitationId;
                                    VendorInvitation.StatusId = vI.StatusId;
                                    VendorInvitation.AdminComment = vI.AdminComment;
                                    VendorInvitation.VendorComment = vI.VendorComment;
                                    VendorInvitation.AdminAmount = vI.AdminAmount;
                                    VendorInvitation.AdminPerc = vI.AdminPerc;
                                    VendorInvitation.Amount = vI.Amount;
                                    userdta.VendorInvitation = new VendorInvitation();
                                    userdta.VendorInvitation = VendorInvitation;
                                }
                            }
                        }
                    }
                    _outUser = _outUser.GroupBy(m => m.UserId).Select(g => g.First()).ToList();
                    return await Task.Run(() => _outUser.ToList());
                }
                else
                {

                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<List<ModelClass.VendorInterest>> GetVendorInterestbyplace(string place)
        {
            try
            {

                List<ModelClass.VendorInterest> _out = new List<ModelClass.VendorInterest>();
                List<Db.VendorInterest> _itemInput = new List<Db.VendorInterest>();
                _itemInput = await _context.VendorInterest.Where(m => m.RecordStatus == "Active" && m.Place == place).ToListAsync();


                if (_itemInput != null)
                {
                    foreach (var itm in _itemInput)
                    {

                        ModelClass.VendorInterest _inUsr = new ModelClass.VendorInterest();
                        ModelClass.EventType _inEve = new ModelClass.EventType();
                        ModelClass.EventSubType _inCom = new EventSubType();
                        List<ModelClass.VendorInterestRate> _inTeam = new List<ModelClass.VendorInterestRate>();
                        //Details from User table 
                        if (itm != null)
                        {

                            ModelClass.LookUpRateType _inR = new ModelClass.LookUpRateType();

                            _inUsr.VendorInterestId = itm.VendorInterestId;
                            _inUsr.PlaceId = itm.PlaceId;
                            _inUsr.UserId = itm.UserId;
                            _inUsr.Place = itm.Place;
                            _inUsr.EventTypeId = itm.EventTypeId;
                            _inUsr.EventSubTypeId = itm.EventSubTypeId;
                            _inUsr.RecordStatus = itm.RecordStatus;

                            var _itemInput5 = await _context.EventType.Where(m => m.EventTypeId == itm.EventTypeId).FirstOrDefaultAsync();
                            if (_itemInput5 != null)
                            {
                                _inEve.EventTypeId = _itemInput5.EventTypeId;
                                _inEve.EventTypeName = _itemInput5.EventTypeName;
                                _inUsr.EventType = new EventType();
                                _inUsr.EventType = _inEve;
                            }
                            var _itemInput6 = await _context.EventSubType.Where(m => m.EventSubTypeId == itm.EventSubTypeId).FirstOrDefaultAsync();
                            if (_itemInput6 != null)
                            {
                                _inCom.EventSubTypeId = _itemInput6.EventSubTypeId;
                                _inCom.SubTypeName = _itemInput6.SubTypeName;
                                _inCom.EventTypeId = _itemInput6.EventTypeId;
                                _inUsr.EventSubType = _inCom;
                            }
                        }

                        //Details from PersonType table
                        var _itemInput1 = await _context.VendorInterestRate.Where(m => m.RecordStatus == "Active" && m.VendorInterestId == itm.VendorInterestId).FirstOrDefaultAsync();
                        if (_itemInput1 != null)
                        {
                            ModelClass.VendorInterestRate _inRate = new ModelClass.VendorInterestRate();
                            _inRate.VendorInterestRateId = _itemInput1.VendorInterestRateId;
                            _inRate.rate = _itemInput1.rate;
                            _inRate.LookupRateTypeId = Convert.ToInt32(_itemInput1.LookupRateTypeId);
                            _inRate.RecordStatus = _itemInput1.RecordStatus;
                            var _itemInput7 = await _context.LookUpRateType.Where(m => m.LookUpRateTypeId == _itemInput1.LookupRateTypeId && m.RecordStatus == "Active").FirstOrDefaultAsync();
                            if (_itemInput7 != null)
                            {
                                ModelClass.LookUpRateType _inLookCom = new LookUpRateType();
                                _inLookCom.LookUpRateTypeId = _itemInput7.LookUpRateTypeId;
                                _inLookCom.RateType = _itemInput7.RateType;
                                _inLookCom.RecordStatus = _itemInput7.RecordStatus;
                                _inRate.LookUpRateType = _inLookCom;
                            }
                            _inTeam.Add(_inRate);
                        }
                        _inUsr.VendorInterestRate = _inTeam;
                        _out.Add(_inUsr);

                    }

                    return await Task.Run(() => _out.ToList());
                }
                else
                {

                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        public bool SaveEventPlaceMapping(EventPlaceViewModelPOST model)
        {

            try
            {
                using (var tran = new TransactionScope())
                {
                    Db.EventType_Place eventPlace = new Db.EventType_Place();
                    eventPlace.EventSubTypeId = model.EventSubTypeId;
                    eventPlace.EventTypeId = model.EventTypeId;
                    eventPlace.IsDeleted = false;
                    eventPlace.PlaceId = model.PlaceId;
                    _context.EventType_Place.Add(eventPlace);
                    _context.SaveChanges();
                    tran.Complete();
                    return true;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool UpdateEventPlaceMapping(EventPlaceViewModelPUT model)
        {

            try
            {
                var eventPlace = _context.EventType_Place.Where(k => k.Id == model.EventypeLocationId).FirstOrDefault();
                if (eventPlace == null)
                    throw new Exception("Cannot find the data.");
                using (var tran = new TransactionScope())
                {
                    eventPlace.EventSubTypeId = model.EventSubTypeId;
                    eventPlace.EventTypeId = model.EventTypeId;
                    eventPlace.IsDeleted = model.IsDeleted;
                    eventPlace.PlaceId = model.PlaceId;
                    _context.SaveChanges();
                    tran.Complete();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public class EventViewModelPost
        {

            public string Name { get; set; }
            public DateTime ExpectedToConduct { get; set; }
            public string EmployeeRange { get; set; }
            public Nullable<long> CorporateId { get; set; }
            public int? ScheduleTypeId { get; set; }
            public string Location { get; set; }
            public List<EventDetailsViewModelPost> EventDetails { get; set; }
        }
        public class EventDetailsViewModelPost
        {
            public Nullable<int> EventTypeId { get; set; }
            public Nullable<int> EventSubTypeId { get; set; }
            public int SurfaceTypeId { get; set; }
            public string EmployeeRange { get; set; }
            public DateTime ExpectedToConduct { get; set; }
            public Nullable<int> ScheduleTypeId { get; set; }
            public string Location { get; set; }

        }
        public class AssignVendorEventPUT
        {
            public long EventId { get; set; }
            public long VendorId { get; set; }
            public int? StatusId { get; set; }
            public int? ModifiedBy { get; set; }
        }
        public class EventViewModelVendorPUT
        {
            public long EventId { get; set; }
            public int StatusId { get; set; }
            public int? ModifiedBy { get; set; }
        }
        public class EventPlaceViewModelPOST
        {
            public int? EventSubTypeId { get; set; }
            public int? EventTypeId { get; set; }
            public int PlaceId { get; set; }
        }
        public class EventPlaceViewModelPUT
        {
            public int EventypeLocationId { get; set; }
            public int? EventSubTypeId { get; set; }
            public int? EventTypeId { get; set; }
            public int PlaceId { get; set; }
            public bool IsDeleted { get; set; }
        }
    }
}
