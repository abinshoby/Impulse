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
    public class EventHandler : IEvent
    {
        private readonly DataContext _context;

        public EventHandler(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> SaveEventDetails(EventViewModelPost modal)
        {
            try
            {
                var status = await _context.EventStatusType.Where(i => i.StatusName == "Pending").FirstOrDefaultAsync();
                Db.Event obj = new Db.Event();
                obj.CreatedBy = (modal.CorporateId != null && modal.CorporateId > 0) ? modal.CorporateId : (int?)null;
                obj.CreatedOn = DateTime.Now;
                obj.StatusId = (status != null) ? status.StatusId : (int?)null;
                obj.Name = modal.Name;
                obj.EmployeeRange = modal.EmployeeRange;
                obj.ExpectedToConduct = modal.ExpectedToConduct;
                obj.IsDeleted = false;
                obj.Location = modal.Location;
                obj.ScheduleTypeId = (modal.ScheduleTypeId != null && modal.ScheduleTypeId > 0) ? modal.ScheduleTypeId : (int?)null;

                var savedData = _context.Event.Add(obj);
                if (modal.EventDetails != null && modal.EventDetails.Count > 0)
                {
                    foreach (var itm in modal.EventDetails)
                    {
                        Db.EventDetails eventD = new Db.EventDetails();
                        eventD.EventId = obj.EventId;
                        eventD.EventTypeId = (itm.EventTypeId != null && itm.EventTypeId > 0) ? itm.EventTypeId : (int?)null;
                        eventD.EventSubTypeId = (itm.EventSubTypeId != null && itm.EventSubTypeId > 0) ? itm.EventSubTypeId : (int?)null;
                        eventD.SurfaceTypeId = itm.SurfaceTypeId;
                        eventD.ScheduleTypeId = (itm.ScheduleTypeId != null && itm.ScheduleTypeId > 0) ? itm.ScheduleTypeId : (int?)null;
                        eventD.Location = itm.Location;
                        eventD.EmployeeRange = itm.EmployeeRange;
                        eventD.ExpectedToConduct = itm.ExpectedToConduct;
                        eventD.IsDeleted = false;
                        obj.CreatedOn = DateTime.Now;
                        _context.EventDetails.Add(eventD);
                    }
                }
                _context.SaveChanges();

                return await Task.Run(() => true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> UpdateEventDetails(EventViewModelPost modal)
        {
            try
            {
                var status = await _context.EventStatusType.Where(i => i.StatusName == "Pending").FirstOrDefaultAsync();
                Db.Event obj = await _context.Event.Where(i => i.EventId == modal.EventID).FirstOrDefaultAsync();
                obj.StatusId = (status != null) ? status.StatusId : (int?)null;
                obj.Name = modal.Name;
                obj.EmployeeRange = modal.EmployeeRange;
                obj.ExpectedToConduct = modal.ExpectedToConduct;
                obj.Location = modal.Location;
                obj.ScheduleTypeId = (modal.ScheduleTypeId != null && modal.ScheduleTypeId > 0) ? modal.ScheduleTypeId : (int?)null;
                _context.SaveChanges();

                if (modal.EventDetails != null && modal.EventDetails.Count > 0)
                {
                    foreach (var itm in modal.EventDetails)
                    {
                        Db.EventDetails eventD = await _context.EventDetails.Where(i => i.EventDetailsId == itm.EventDetailsID).FirstOrDefaultAsync();
                        eventD.EventTypeId = (itm.EventTypeId != null && itm.EventTypeId > 0) ? itm.EventTypeId : (int?)null;
                        eventD.EventSubTypeId = (itm.EventSubTypeId != null && itm.EventSubTypeId > 0) ? itm.EventSubTypeId : (int?)null;
                        eventD.SurfaceTypeId = itm.SurfaceTypeId;
                        eventD.ScheduleTypeId = (itm.ScheduleTypeId != null && itm.ScheduleTypeId > 0) ? itm.ScheduleTypeId : (int?)null;
                        eventD.Location = itm.Location;
                        eventD.EmployeeRange = itm.EmployeeRange;
                        eventD.ExpectedToConduct = itm.ExpectedToConduct;
                        _context.SaveChanges();

                    }
                }

                return await Task.Run(() => true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> Delete(int id)
        {
            try
            {
                var status = await _context.EventStatusType.Where(i => i.StatusName == "Pending").FirstOrDefaultAsync();
                Db.Event obj = await _context.Event.Where(i => i.EventId == id).FirstOrDefaultAsync();
                obj.IsDeleted = true;
                _context.SaveChanges();
                var eventDetails = await _context.EventDetails.Where(i => i.EventId == id).ToListAsync();
                if (eventDetails != null && eventDetails.Count > 0)
                {
                    foreach (var itm in eventDetails)
                    {
                        Db.EventDetails eventDetail = await _context.EventDetails.Where(i => i.EventDetailsId == itm.EventDetailsId).FirstOrDefaultAsync();
                        eventDetail.IsDeleted = true;
                        _context.SaveChanges();

                    }
                }

                return await Task.Run(() => true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        const string InProgress = "InProgress";
        public async Task<bool> UpdateEventStatus(AssignVendorEventPUT modal)
        {
            try
            {

                Db.Event obj = _context.Event.Find(modal.EventId);
                if (obj == null)
                    throw new Exception("Requested event empty");
                obj.StatusId = modal.StatusId;
                obj.ModifiedBy = modal.ModifiedBy;
                obj.ModifiedOn = DateTime.Now;
                _context.SaveChanges();


                return await Task.Run(() => true);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> AssingnVendorsToEvent(List<VendorInvitation> modal)
        {
            try
            {

                if (modal != null && modal.Count > 0)
                {
                    foreach (var itm in modal)
                    {
                        var item = await _context.VendorInvitation.Where(i => i.EventDetailsId == itm.EventDetailsId && i.VendorId == itm.VendorId && i.IsDeleted == false).FirstOrDefaultAsync();
                        if (item == null)
                        {
                            Db.VendorInvitation eventD = new Db.VendorInvitation();
                            eventD.VendorId = itm.VendorId;
                            eventD.EventDetailsId = itm.EventDetailsId;
                            eventD.StatusId = itm.StatusId;
                            eventD.AdminComment = itm.AdminComment;
                            eventD.VendorComment = itm.VendorComment;
                            eventD.Amount = itm.Amount;

                            eventD.IsDeleted = false;
                            eventD.CreatedBy = itm.CreatedBy;
                            eventD.CreatedOn = System.DateTime.Now;
                            _context.VendorInvitation.Add(eventD);
                            var eventId = await _context.EventDetails.Where(x => x.EventDetailsId == itm.EventDetailsId).FirstOrDefaultAsync();
                            Db.Event _data = _context.Event.Find(eventId.EventId);
                            _data.StatusId = 7;
                            _context.SaveChanges();
                        }
                        else
                        {
                            await UpdateVendorsInvitationStatus(itm);
                        }
                    }
                }
                _context.SaveChanges();

                return await Task.Run(() => true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> UpdateVendorsInvitationStatus(VendorInvitation modal)
        {
            try
            {
                if (modal != null)
                {
                    Db.VendorInvitation eventD = await _context.VendorInvitation.Where(x => x.EventDetailsId == modal.EventDetailsId && x.VendorId == modal.VendorId).FirstOrDefaultAsync();
                    eventD.StatusId = modal.StatusId;
                    if (modal.AdminComment != null)
                    {
                        eventD.AdminComment = modal.AdminComment;
                    }
                    if (modal.VendorComment != null)
                    {
                        eventD.VendorComment = modal.VendorComment;
                    }
                    if (modal.Amount != null)
                    {
                        eventD.Amount = modal.Amount;
                    }
                    if (modal.AdminAmount != null)
                    {
                        eventD.AdminAmount = modal.AdminAmount;
                        eventD.AdminPerc = modal.AdminPerc;
                    }
                    _context.SaveChanges();
                    if (modal.StatusId == 3)
                    {
                        var data = await _context.VendorInvitation.Where(x => x.EventDetailsId == modal.EventDetailsId).ToListAsync();
                        bool _result = false;
                        if (data != null)
                        {
                            foreach (var itm in data)
                            {
                                if (itm.StatusId == 3)
                                {
                                    _result = true;
                                    break;
                                }
                                else
                                {
                                    _result = false;
                                }
                            }
                        }
                        if (_result == true)
                        {
                            var eventId = await _context.EventDetails.Where(x => x.EventDetailsId == modal.EventDetailsId).FirstOrDefaultAsync();
                            Db.Event _data = _context.Event.Find(eventId.EventId);
                            _data.StatusId = 3;
                            _context.SaveChanges();
                        }

                    }
                    if (modal.StatusId == 7)
                    {

                        var eventId = await _context.EventDetails.Where(x => x.EventDetailsId == modal.EventDetailsId).FirstOrDefaultAsync();
                        Db.Event _data = _context.Event.Find(eventId.EventId);
                        _data.StatusId = 7;
                        _context.SaveChanges();

                    }
                    if (modal.StatusId == 9)
                    {

                        var eventId = await _context.EventDetails.Where(x => x.EventDetailsId == modal.EventDetailsId).FirstOrDefaultAsync();
                        Db.Event _data = _context.Event.Find(eventId.EventId);
                        _data.StatusId = 9;
                        _context.SaveChanges();

                    }

                }

                return await Task.Run(() => true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ModelClass.EventViewModel>> GetAllEventByCorporateId(int Id)
        {
            try
            {
                List<ModelClass.EventViewModel> _list = new List<ModelClass.EventViewModel>();
                var itm2 = await _context.VendorInvitation.Where(i => i.VendorId == Id && i.IsDeleted == false && (i.StatusId == 3 || i.StatusId == 5 || i.StatusId == 9)).OrderByDescending(x => x.VendorInvitationId).ToListAsync();
                if (itm2 != null && itm2.Count > 0)
                {
                    foreach (var U in itm2)
                    {
                        var itEv = await _context.EventDetails.Where(i => i.EventDetailsId == U.EventDetailsId && i.IsDeleted == false).FirstOrDefaultAsync();
                        var itm = _context.Event.Where(i => i.EventId == itEv.EventId && i.IsDeleted == false).FirstOrDefault();
                        if ((itm != null))
                        {
                            EventViewModel obj = new EventViewModel();
                            obj.EventId = itm.EventId;
                            obj.Name = itm.Name;
                            obj.StatusId = itm.StatusId;
                            obj.StatusName = _context.EventStatusType.Find(itm.StatusId).StatusName;
                            obj.CorporateId = itm.CreatedBy;
                            obj.CreatedUser = await GetUser(Convert.ToInt32(itm.CreatedBy));
                            obj.EmployeeRange = itm.EmployeeRange;
                            obj.ExpectedToConduct = itm.ExpectedToConduct;
                            obj.Location = itm.Location;
                            obj.CreatedOn = itm.CreatedOn;
                            obj.ScheduleType = (itm.ScheduleTypeId != null) ? _context.ScheduleType.Find(itm.ScheduleTypeId).ScheduleTypeName : string.Empty;
                            obj.ScheduleTypeId = itm.ScheduleTypeId;
                            VendorInvitation _bdta = new VendorInvitation();
                            _bdta.VendorInvitationId = U.VendorInvitationId;
                            _bdta.EventDetailsId = U.EventDetailsId;
                            _bdta.VendorId = U.VendorId;
                            _bdta.AdminComment = U.AdminComment;
                            _bdta.VendorComment = U.VendorComment;
                            _bdta.StatusId = U.StatusId;
                            _bdta.Amount = U.AdminAmount;
                            _bdta.Amount = U.AdminPerc;
                            _bdta.StatusName = _context.EventStatusType.Find(Convert.ToInt32(U.StatusId)).StatusName;
                            obj.VendorInvitation = _bdta;
                            List<EventDetailsViewModel> _dataList = new List<EventDetailsViewModel>();
                            var itm1 = await _context.EventDetails.Where(i => i.EventId == itm.EventId && i.IsDeleted == false).ToListAsync();
                            if (itm1 != null && itm1.Count > 0)
                            {
                                foreach (var k in itm1)
                                {
                                    EventDetailsViewModel _data = new EventDetailsViewModel();
                                    _data.EventDetailsId = k.EventDetailsId;
                                    _data.EventSubTypeId = k.EventSubTypeId;
                                    _data.EventSubType = (k.EventSubTypeId == null) ? string.Empty : _context.EventSubType.Find(k.EventSubTypeId).SubTypeName;
                                    _data.EventTypeId = k.EventTypeId;
                                    _data.EventType = (k.EventTypeId == null) ? string.Empty : _context.EventType.Find(k.EventTypeId).EventTypeName;
                                    _data.SurfaceTypeId = k.SurfaceTypeId;
                                    _data.SurfaceType = "";
                                    _data.EmployeeRange = k.EmployeeRange;
                                    _data.ExpectedToConduct = k.ExpectedToConduct;
                                    _data.Location = k.Location;
                                    _dataList.Add(_data);
                                }
                                obj.EventDetails = _dataList;
                            }
                            _list.Add(obj);
                        }
                    }

                }
                return await Task.Run(() => _list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<EventViewModel> GetEvent(int id)
        {
            try
            {
                var itm = _context.Event.Where(i => i.EventId == id && i.IsDeleted == false).FirstOrDefault();
                EventViewModel obj = new EventViewModel();
                obj.EventId = itm.EventId;
                obj.Name = itm.Name;

                obj.StatusId = itm.StatusId;
                obj.StatusName = _context.EventStatusType.Find(itm.StatusId).StatusName;
                obj.CorporateId = itm.CreatedBy;
                obj.CreatedUser = await GetUser(Convert.ToInt32(itm.CreatedBy));
                obj.EmployeeRange = itm.EmployeeRange;
                obj.ExpectedToConduct = itm.ExpectedToConduct;
                obj.Location = itm.Location;
                obj.CreatedOn = itm.CreatedOn;
                obj.ScheduleType = (itm.ScheduleTypeId != null) ? _context.ScheduleType.Find(itm.ScheduleTypeId).ScheduleTypeName : string.Empty;
                obj.ScheduleTypeId = itm.ScheduleTypeId;
                List<EventDetailsViewModel> _dataList = new List<EventDetailsViewModel>();
                var itm1 = await _context.EventDetails.Where(i => i.EventId == id && i.IsDeleted == false).ToListAsync();
                if (itm1 != null && itm1.Count > 0)
                {
                    foreach (var k in itm1)
                    {
                        EventDetailsViewModel _data = new EventDetailsViewModel();
                        _data.EventDetailsId = k.EventDetailsId;
                        _data.EventSubTypeId = k.EventSubTypeId;
                        _data.EventSubType = (k.EventSubTypeId == null) ? string.Empty : _context.EventSubType.Find(k.EventSubTypeId).SubTypeName;
                        _data.EventTypeId = k.EventTypeId;
                        _data.EventType = (k.EventTypeId == null) ? string.Empty : _context.EventType.Find(k.EventTypeId).EventTypeName;
                        _data.SurfaceTypeId = k.SurfaceTypeId;
                        _data.SurfaceType = "";
                        _data.EmployeeRange = k.EmployeeRange;
                        _data.ExpectedToConduct = k.ExpectedToConduct;
                        _data.Location = k.Location;
                        List<User> _userList = new List<User>();
                        if (obj.StatusId == 3)
                        {
                            var itm2 = await _context.VendorInvitation.Where(i => i.EventDetailsId == k.EventDetailsId && i.IsDeleted == false && i.StatusId == 3).ToListAsync();
                            if (itm2 != null && itm2.Count > 0)
                            {
                                foreach (var U in itm2)
                                {
                                    User _user = new User();
                                    _user = await GetUser(Convert.ToInt32(U.VendorId));
                                    VendorInvitation _bdta = new VendorInvitation();
                                    _bdta.VendorInvitationId = U.VendorInvitationId;
                                    _bdta.EventDetailsId = U.EventDetailsId;
                                    _bdta.VendorId = U.VendorId;
                                    _bdta.StatusId = U.StatusId;
                                    _bdta.AdminComment = U.AdminComment;
                                    _bdta.VendorComment = U.VendorComment;
                                    _bdta.AdminAmount = U.AdminAmount;
                                    _bdta.AdminPerc = U.AdminPerc;
                                    _bdta.Amount = U.Amount;
                                    _bdta.StatusName = _context.EventStatusType.Find(Convert.ToInt32(U.StatusId)).StatusName;
                                    _user.VendorInvitation = _bdta;
                                    _userList.Add(_user);

                                }
                                _data.Assignedser = _userList;
                            }
                        }
                        else if (obj.StatusId == 9)
                        {
                            var itm2 = await _context.VendorInvitation.Where(i => i.EventDetailsId == k.EventDetailsId && i.IsDeleted == false && i.StatusId == 9).ToListAsync();
                            if (itm2 != null && itm2.Count > 0)
                            {
                                foreach (var U in itm2)
                                {
                                    User _user = new User();
                                    _user = await GetUser(Convert.ToInt32(U.VendorId));
                                    VendorInvitation _bdta = new VendorInvitation();
                                    _bdta.VendorInvitationId = U.VendorInvitationId;
                                    _bdta.EventDetailsId = U.EventDetailsId;
                                    _bdta.VendorId = U.VendorId;
                                    _bdta.StatusId = U.StatusId;
                                    _bdta.AdminComment = U.AdminComment;
                                    _bdta.VendorComment = U.VendorComment;
                                    _bdta.AdminAmount = U.AdminAmount;
                                    _bdta.AdminPerc = U.AdminPerc;
                                    _bdta.Amount = U.Amount;
                                    _bdta.StatusName = _context.EventStatusType.Find(Convert.ToInt32(U.StatusId)).StatusName;
                                    _user.VendorInvitation = _bdta;
                                    _userList.Add(_user);

                                }
                                _data.Assignedser = _userList;
                            }
                        }
                        else
                        {
                            var itm2 = await _context.VendorInvitation.Where(i => i.EventDetailsId == k.EventDetailsId && i.IsDeleted == false).ToListAsync();
                            if (itm2 != null && itm2.Count > 0)
                            {
                                foreach (var U in itm2)
                                {
                                    User _user = new User();
                                    _user = await GetUser(Convert.ToInt32(U.VendorId));
                                    VendorInvitation _bdta = new VendorInvitation();
                                    _bdta.VendorInvitationId = U.VendorInvitationId;
                                    _bdta.EventDetailsId = U.EventDetailsId;
                                    _bdta.VendorId = U.VendorId;
                                    _bdta.StatusId = U.StatusId;
                                    _bdta.AdminComment = U.AdminComment;
                                    _bdta.VendorComment = U.VendorComment;
                                    _bdta.Amount = U.Amount;
                                    _bdta.Amount = U.AdminAmount;
                                    _bdta.Amount = U.AdminPerc;
                                    _bdta.StatusName = _context.EventStatusType.Find(Convert.ToInt32(U.StatusId)).StatusName;
                                    _user.VendorInvitation = _bdta;
                                    _userList.Add(_user);

                                }
                                _data.Assignedser = _userList;
                            }
                        }
                        _dataList.Add(_data);

                    }
                    obj.EventDetails = _dataList;
                }


                return await Task.Run(() => obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<EventViewModel> GetCorporateEvent(int id)
        {
            try
            {
                var itm = _context.Event.Where(i => i.EventId == id && i.IsDeleted == false).FirstOrDefault();
                EventViewModel obj = new EventViewModel();
                obj.EventId = itm.EventId;
                obj.Name = itm.Name;
                obj.StatusId = itm.StatusId;
                obj.StatusName = _context.EventStatusType.Find(itm.StatusId).StatusName;
                obj.CorporateId = itm.CreatedBy;
                obj.CreatedUser = await GetUser(Convert.ToInt32(itm.CreatedBy));
                obj.EmployeeRange = itm.EmployeeRange;
                obj.ExpectedToConduct = itm.ExpectedToConduct;
                obj.Location = itm.Location;
                obj.CreatedOn = itm.CreatedOn;
                obj.ScheduleType = (itm.ScheduleTypeId != null) ? _context.ScheduleType.Find(itm.ScheduleTypeId).ScheduleTypeName : string.Empty;
                obj.ScheduleTypeId = itm.ScheduleTypeId;
                List<EventDetailsViewModel> _dataList = new List<EventDetailsViewModel>();
                var itm1 = await _context.EventDetails.Where(i => i.EventId == id && i.IsDeleted == false).ToListAsync();
                if (itm1 != null && itm1.Count > 0)
                {
                    foreach (var k in itm1)
                    {
                        EventDetailsViewModel _data = new EventDetailsViewModel();
                        _data.EventDetailsId = k.EventDetailsId;
                        _data.EventSubTypeId = k.EventSubTypeId;
                        _data.EventSubType = (k.EventSubTypeId == null) ? string.Empty : _context.EventSubType.Find(k.EventSubTypeId).SubTypeName;
                        _data.EventTypeId = k.EventTypeId;
                        _data.EventType = (k.EventTypeId == null) ? string.Empty : _context.EventType.Find(k.EventTypeId).EventTypeName;
                        _data.SurfaceTypeId = k.SurfaceTypeId;
                        _data.SurfaceType = "";
                        _data.EmployeeRange = k.EmployeeRange;
                        _data.ExpectedToConduct = k.ExpectedToConduct;
                        _data.Location = k.Location;
                        List<User> _userList = new List<User>();
                        if (obj.StatusId == 3)
                        {
                            var itm2 = await _context.VendorInvitation.Where(i => i.EventDetailsId == k.EventDetailsId && i.IsDeleted == false && i.StatusId == 3).ToListAsync();
                            if (itm2 != null && itm2.Count > 0)
                            {
                                foreach (var U in itm2)
                                {
                                    User _user = new User();
                                    _user = await GetUser(Convert.ToInt32(U.VendorId));
                                    VendorInvitation _bdta = new VendorInvitation();
                                    _bdta.VendorInvitationId = U.VendorInvitationId;
                                    _bdta.EventDetailsId = U.EventDetailsId;
                                    _bdta.VendorId = U.VendorId;
                                    _bdta.StatusId = U.StatusId;
                                    _bdta.AdminComment = U.AdminComment;
                                    _bdta.VendorComment = U.VendorComment;
                                    _bdta.AdminAmount = U.AdminAmount;
                                    _bdta.AdminPerc = U.AdminPerc;
                                    _bdta.StatusName = _context.EventStatusType.Find(Convert.ToInt32(U.StatusId)).StatusName;
                                    _user.VendorInvitation = _bdta;
                                    _userList.Add(_user);

                                }
                                _data.Assignedser = _userList;
                            }
                        }
                        else
                        {
                            var itm2 = await _context.VendorInvitation.Where(i => i.EventDetailsId == k.EventDetailsId && i.IsDeleted == false && (i.StatusId == 5 || i.StatusId == 9)).ToListAsync();
                            if (itm2 != null && itm2.Count > 0)
                            {
                                foreach (var U in itm2)
                                {
                                    User _user = new User();
                                    _user = await GetUser(Convert.ToInt32(U.VendorId));
                                    VendorInvitation _bdta = new VendorInvitation();
                                    _bdta.VendorInvitationId = U.VendorInvitationId;
                                    _bdta.EventDetailsId = U.EventDetailsId;
                                    _bdta.VendorId = U.VendorId;
                                    _bdta.StatusId = U.StatusId;
                                    _bdta.AdminComment = U.AdminComment;
                                    _bdta.VendorComment = U.VendorComment;
                                    _bdta.AdminAmount = U.AdminAmount;
                                    _bdta.AdminPerc = U.AdminPerc;
                                    _bdta.StatusName = _context.EventStatusType.Find(Convert.ToInt32(U.StatusId)).StatusName;
                                    _user.VendorInvitation = _bdta;
                                    _userList.Add(_user);

                                }
                                _data.Assignedser = _userList;
                            }
                        }
                        _dataList.Add(_data);

                    }
                    obj.EventDetails = _dataList;
                }


                return await Task.Run(() => obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ModelClass.User> GetUser(int Id)
        {
            try
            {

                ModelClass.User _inUsr = new ModelClass.User();
                ModelClass.Team _inTeam = new ModelClass.Team();
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
                        _inUsr.Password = itm.Password;
                        _inUsr.LookUpUserTypeId = itm.LookUpUserTypeId;
                        _inUsr.LookUpUserStatusId = itm.LookUpUserStatusId;
                        _inUsr.RecordStatus = itm.RecordStatus;
                        _inUsr.VendorInterest = new List<ModelClass.VendorInterest>();

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
        public string GetUserNameById(int userId)
        {
            var user = _context.User.Find(userId);
            if (user != null)
                return user.UserName;
            return string.Empty;
        }
        public async Task<List<ModelClass.EventViewModel>> GetAllByVendorId(int Id)
        {
            try
            {
                List<ModelClass.EventViewModel> _list = new List<ModelClass.EventViewModel>();
                var itm2 = await _context.VendorInvitation.Where(i => i.VendorId == Id && i.IsDeleted == false).OrderByDescending(x => x.VendorInvitationId).ToListAsync();
                if (itm2 != null && itm2.Count > 0)
                {
                    foreach (var U in itm2)
                    {
                        var itEv = await _context.EventDetails.Where(i => i.EventDetailsId == U.EventDetailsId && i.IsDeleted == false).FirstOrDefaultAsync();
                        var itm = _context.Event.Where(i => i.EventId == itEv.EventId && i.IsDeleted == false).FirstOrDefault();
                        EventViewModel obj = new EventViewModel();
                        obj.EventId = itm.EventId;
                        obj.Name = itm.Name;
                        obj.StatusId = itm.StatusId;
                        obj.StatusName = _context.EventStatusType.Find(itm.StatusId).StatusName;
                        obj.CorporateId = itm.CreatedBy;
                        obj.CreatedUser = await GetUser(Convert.ToInt32(itm.CreatedBy));
                        obj.EmployeeRange = itm.EmployeeRange;
                        obj.ExpectedToConduct = itm.ExpectedToConduct;
                        obj.Location = itm.Location;
                        obj.CreatedOn = itm.CreatedOn;
                        obj.ScheduleType = (itm.ScheduleTypeId != null) ? _context.ScheduleType.Find(itm.ScheduleTypeId).ScheduleTypeName : string.Empty;
                        obj.ScheduleTypeId = itm.ScheduleTypeId;
                        if (itm.StatusId == 3 && U.StatusId == 3)
                        {
                            obj.FinalVendorStatus = _context.EventStatusType.Find(itm.StatusId).StatusName;
                        }
                        else if (itm.StatusId == 3 && U.StatusId != 3)
                        {
                            obj.FinalVendorStatus = _context.EventStatusType.Find(6).StatusName;
                        }
                        else
                        {
                            obj.FinalVendorStatus = _context.EventStatusType.Find(7).StatusName;

                        }
                        VendorInvitation _bdta = new VendorInvitation();
                        _bdta.VendorInvitationId = U.VendorInvitationId;
                        _bdta.EventDetailsId = U.EventDetailsId;
                        _bdta.VendorId = U.VendorId;
                        _bdta.AdminComment = U.AdminComment;
                        _bdta.VendorComment = U.VendorComment;
                        _bdta.StatusId = U.StatusId;
                        _bdta.Amount = U.Amount;

                        _bdta.StatusName = _context.EventStatusType.Find(Convert.ToInt32(U.StatusId)).StatusName;
                        obj.VendorInvitation = _bdta;
                        List<EventDetailsViewModel> _dataList = new List<EventDetailsViewModel>();
                        var itm1 = await _context.EventDetails.Where(i => i.EventId == itm.EventId && i.IsDeleted == false).ToListAsync();
                        if (itm1 != null && itm1.Count > 0)
                        {
                            foreach (var k in itm1)
                            {
                                EventDetailsViewModel _data = new EventDetailsViewModel();
                                _data.EventDetailsId = k.EventDetailsId;
                                _data.EventSubTypeId = k.EventSubTypeId;
                                _data.EventSubType = (k.EventSubTypeId == null) ? string.Empty : _context.EventSubType.Find(k.EventSubTypeId).SubTypeName;
                                _data.EventTypeId = k.EventTypeId;
                                _data.EventType = (k.EventTypeId == null) ? string.Empty : _context.EventType.Find(k.EventTypeId).EventTypeName;
                                _data.SurfaceTypeId = k.SurfaceTypeId;
                                _data.SurfaceType = "";
                                _data.EmployeeRange = k.EmployeeRange;
                                _data.ExpectedToConduct = k.ExpectedToConduct;
                                _data.Location = k.Location;

                                var itm7 = await _context.VendorInvitation.Where(i => i.EventDetailsId == k.EventDetailsId && i.VendorId == Id && i.IsDeleted == false).OrderByDescending(x => x.VendorInvitationId).FirstOrDefaultAsync();
                                VendorInvitation _bdta1 = new VendorInvitation();
                                _bdta1.VendorInvitationId = itm7.VendorInvitationId;
                                _bdta1.EventDetailsId = itm7.EventDetailsId;
                                _bdta1.VendorId = itm7.VendorId;
                                _bdta1.AdminComment = itm7.AdminComment;
                                _bdta1.VendorComment = itm7.VendorComment;
                                _bdta1.StatusId = itm7.StatusId;
                                _bdta1.Amount = itm7.Amount;

                                _bdta1.StatusName = _context.EventStatusType.Find(Convert.ToInt32(itm7.StatusId)).StatusName;
                                _data.VendorInvitation = _bdta1;
                                _dataList.Add(_data);
                            }
                            obj.EventDetails = _dataList;
                        }
                        _list.Add(obj);

                    }

                }
                return await Task.Run(() => _list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<EventViewModel> GetVendorEvent(int Eventid, int vID)
        {
            try
            {
                var itm = _context.Event.Where(i => i.EventId == Eventid && i.IsDeleted == false).FirstOrDefault();
                EventViewModel obj = new EventViewModel();
                obj.EventId = itm.EventId;
                obj.Name = itm.Name;
                obj.StatusId = itm.StatusId;
                obj.StatusName = _context.EventStatusType.Find(itm.StatusId).StatusName;
                obj.CorporateId = itm.CreatedBy;
                obj.CreatedUser = await GetUser(Convert.ToInt32(itm.CreatedBy));
                obj.EmployeeRange = itm.EmployeeRange;
                obj.ExpectedToConduct = itm.ExpectedToConduct;
                obj.Location = itm.Location;
                obj.CreatedOn = itm.CreatedOn;
                obj.ScheduleType = (itm.ScheduleTypeId != null) ? _context.ScheduleType.Find(itm.ScheduleTypeId).ScheduleTypeName : string.Empty;
                obj.ScheduleTypeId = itm.ScheduleTypeId;

                List<EventDetailsViewModel> _dataList = new List<EventDetailsViewModel>();
                var itm1 = await _context.EventDetails.Where(i => i.EventId == Eventid && i.IsDeleted == false).ToListAsync();
                if (itm1 != null && itm1.Count > 0)
                {
                    foreach (var k in itm1)
                    {
                        EventDetailsViewModel _data = new EventDetailsViewModel();
                        _data.EventDetailsId = k.EventDetailsId;
                        var itm2 = await _context.VendorInvitation.Where(i => i.EventDetailsId == k.EventDetailsId && i.VendorId == vID && i.IsDeleted == false).FirstOrDefaultAsync();
                        if (itm2 != null)
                        {
                            _data.EventSubTypeId = k.EventSubTypeId;
                            _data.EventSubType = (k.EventSubTypeId == null) ? string.Empty : _context.EventSubType.Find(k.EventSubTypeId).SubTypeName;
                            _data.EventTypeId = k.EventTypeId;
                            _data.EventType = (k.EventTypeId == null) ? string.Empty : _context.EventType.Find(k.EventTypeId).EventTypeName;
                            _data.SurfaceTypeId = k.SurfaceTypeId;
                            _data.SurfaceType = "";
                            _data.EmployeeRange = k.EmployeeRange;
                            _data.ExpectedToConduct = k.ExpectedToConduct;
                            _data.Location = k.Location;
                            VendorInvitation _bdta = new VendorInvitation();
                            _bdta.VendorInvitationId = itm2.VendorInvitationId;
                            _bdta.EventDetailsId = itm2.EventDetailsId;
                            _bdta.VendorId = itm2.VendorId;
                            _bdta.StatusId = itm2.StatusId;
                            _bdta.AdminComment = itm2.AdminComment;
                            _bdta.VendorComment = itm2.VendorComment;
                            _bdta.Amount = itm2.Amount;
                            _bdta.StatusName = _context.EventStatusType.Find(Convert.ToInt32(itm2.StatusId)).StatusName;
                            obj.VendorInvitation = _bdta;
                            _data.VendorInvitation = _bdta;
                            _dataList.Add(_data);
                            if (itm.StatusId == 3 && itm2.StatusId == 3)
                            {
                                obj.FinalVendorStatus = _context.EventStatusType.Find(itm.StatusId).StatusName;
                            }
                            else if (itm.StatusId == 3 && itm2.StatusId != 3)
                            {
                                obj.FinalVendorStatus = _context.EventStatusType.Find(6).StatusName;
                            }
                            else
                            {
                                obj.FinalVendorStatus = _context.EventStatusType.Find(7).StatusName;

                            }

                        }

                    }
                    obj.EventDetails = _dataList;
                }


                return await Task.Run(() => obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
