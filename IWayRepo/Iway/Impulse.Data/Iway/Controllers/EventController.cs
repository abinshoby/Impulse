using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Impulse.Model;
using Iway.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Iway.Controllers
{
    public class EventController : Controller
    {
        private readonly IToken _token;
        private readonly ILogin _login;
        private readonly IEvent _event;
        public const string SessionToken = "Token";
        public const string SessionUser = "User";

        public EventController(IToken token, ILogin login, IEvent _event)
        {
            this._token = token;
            this._login = login;
            this._event = _event;
        }
        public async Task<string> getToken()
        {
            string tkn = await _token.GetToken();
            return tkn;

        }
        #region Common
        public async Task<PartialViewResult> _getEventSubType(int EventId)
        {
            EventViewModelPost _in = new EventViewModelPost();
            List<EventSubTypeModel> _input = new List<EventSubTypeModel>();
            var value = HttpContext.Session.GetString(SessionToken);
            if (string.IsNullOrEmpty(value))
            {
                string token = await getToken();
                HttpContext.Session.SetString(SessionToken, token);
                value = HttpContext.Session.GetString(SessionToken);
            }
            _input = await _event.GetEventSubType(value, EventId);

            List<SelectListItem> ddlList = new List<SelectListItem>();
            foreach (var item in _input)
            {
                SelectListItem _data = new SelectListItem();
                _data.Text = item.SubTypeName;
                _data.Value = item.EventSubTypeId.ToString();
                ddlList.Add(_data);
            }
            _in.ddlEventSubType = ddlList;
            return PartialView(_in);
        }
        #endregion

        #region Admin
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Index()
        {
            List<EventViewModel> _in = new List<EventViewModel>();
            var value = HttpContext.Session.GetString(SessionToken);
            if (string.IsNullOrEmpty(value))
            {
                string _token = await getToken();
                HttpContext.Session.SetString(SessionToken, _token);
                value = HttpContext.Session.GetString(SessionToken);
            }
            _in = await _event.GetAdminEventList(value);
            return View(_in);
        }
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Details(int id)
        {
            EventViewModel _in = new EventViewModel();
            var value = HttpContext.Session.GetString(SessionToken);
            if (string.IsNullOrEmpty(value))
            {
                string _token = await getToken();
                HttpContext.Session.SetString(SessionToken, _token);
                value = HttpContext.Session.GetString(SessionToken);
            }
            _in = await _event.GetEventDetailsWithEventId(value, id);
            return View(_in);
        }
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetAdminEvent()
        {
            List<EventViewModel> _in = new List<EventViewModel>();
            var value = HttpContext.Session.GetString(SessionToken);
            if (string.IsNullOrEmpty(value))
            {
                string _token = await getToken();
                HttpContext.Session.SetString(SessionToken, _token);
                value = HttpContext.Session.GetString(SessionToken);
            }
            _in = await _event.GetAdminEventList(value);
            return View(_in);
        }
        public async Task<PartialViewResult> _getVendorListForAssign(int EID, int ESId, int EventDetailsId)
        {
            EventDetailsViewModel _eve = new EventDetailsViewModel();
            _eve.EventDetailsId = EventDetailsId;
            List<User> _in = new List<User>();
            var value = HttpContext.Session.GetString(SessionToken);
            if (string.IsNullOrEmpty(value))
            {
                string _token = await getToken();
                HttpContext.Session.SetString(SessionToken, _token);
                value = HttpContext.Session.GetString(SessionToken);
            }
            _in = await _event.GetVendorInterestUser(value, EID, ESId, EventDetailsId);
            _eve.UserListForAssign = new List<User>();
            _eve.UserListForAssign = _in;
            return PartialView(_eve);
        }
        [HttpPost]
        public async Task<ActionResult> AssignVendorToEvent([FromBody] AssignVendorEvent _input)
        {
            List<VendorInvitation> _list = new List<VendorInvitation>();
            foreach (var item in _input.VIDetails)
            {
                VendorInvitation model = new VendorInvitation();
                model.VendorId = item.UserId;
                model.EventDetailsId = item.EventDetailsId;
                model.StatusId = 7;
                model.AdminComment = "";
                model.VendorComment = "";
                _list.Add(model);
            }
            var value = HttpContext.Session.GetString(SessionToken);
            if (string.IsNullOrEmpty(value))
            {
                string token = await getToken();
                HttpContext.Session.SetString(SessionToken, token);
                value = HttpContext.Session.GetString(SessionToken);
            }
            bool _out = await _event.AssingnVendorsToEvent(value, _list);
            if (_out == true)
            {

                return Json(_input.EventId);
            }

            return null;
        }


        [HttpPost]
        public async Task<JsonResult> SaveAdminAcceptVendorForm([FromBody] VendorInvitation model)
        {
            model.StatusId = 5;
            var value = HttpContext.Session.GetString(SessionToken);
            if (string.IsNullOrEmpty(value))
            {
                string token = await getToken();
                HttpContext.Session.SetString(SessionToken, token);
                value = HttpContext.Session.GetString(SessionToken);
            }
            bool _out = await _event.UpdateVendorsInvitationStatus(value, model);
            return Json(1);
        }
        public async Task<ActionResult> AdminConfirmedEvent([FromBody] AssignVendorEvent _input)
        {
            List<VendorInvitation> _list = new List<VendorInvitation>();
            foreach (var item in _input.VIDetails)
            {
                VendorInvitation model = new VendorInvitation();
                model.VendorId = item.UserId;
                model.EventDetailsId = item.EventDetailsId;
                model.StatusId = 3;
                _list.Add(model);
            }
            var value = HttpContext.Session.GetString(SessionToken);
            if (string.IsNullOrEmpty(value))
            {
                string token = await getToken();
                HttpContext.Session.SetString(SessionToken, token);
                value = HttpContext.Session.GetString(SessionToken);
            }
            bool _out = await _event.AssingnVendorsToEvent(value, _list);
            if (_out == true)
            {

                return Json(_input.EventId);
            }

            return null;
        }
        #endregion

        #region Corporate 
        [HttpPost]
        public async Task<ActionResult> SaveEvent(EventViewModelPost _input)
        {
            List<EventDetailsViewModelPost> _list = new List<EventDetailsViewModelPost>();
            if (_input.EventSubTypeList.Count() > 0)
            {
                foreach (int item in _input.EventSubTypeList)
                {
                    EventDetailsViewModelPost model = new EventDetailsViewModelPost();
                    model.EventTypeId = _input.EventTypeId;
                    model.EventSubTypeId = item;
                    model.SurfaceTypeId = 1;
                    model.Location = _input.Location;
                    model.EmployeeRange = _input.EmployeeRange;
                    model.ExpectedToConduct = _input.ExpectedToConduct;
                    model.ScheduleTypeId = Convert.ToInt32(_input.ScheduleType);
                    _list.Add(model);
                }
            }
            _input.EventDetails = new List<EventDetailsViewModelPost>();
            _input.EventDetails = _list;
            _input.ScheduleTypeId = Convert.ToInt32(_input.ScheduleType);
            var value = HttpContext.Session.GetString(SessionToken);
            if (string.IsNullOrEmpty(value))
            {
                string token = await getToken();
                HttpContext.Session.SetString(SessionToken, token);
                value = HttpContext.Session.GetString(SessionToken);
            }
            bool _out = await _event.AddEventDetails(value, _input);
            if (_out == true)
            {
                return RedirectToAction("CorporateIndex", "Dashboard", new { @id = _input.CorporateId });
                //return Json(_input.CorporateId);
            }

            return null;
        }
        public async Task<PartialViewResult> _AddEvent(int corporateId)
        {
            EventViewModelPost _in = new EventViewModelPost();
            _in.CorporateId = corporateId;
            List<EventType> _input = new List<EventType>();
            List<EventSubTypeModel> _inputS = new List<EventSubTypeModel>();
            var value = HttpContext.Session.GetString(SessionToken);
            if (string.IsNullOrEmpty(value))
            {
                string token = await getToken();
                HttpContext.Session.SetString(SessionToken, token);
                value = HttpContext.Session.GetString(SessionToken);
            }
            _input = await _event.GetEventType(value);
            _inputS = await _event.GetEventSubType(value, _input.FirstOrDefault().EventTypeId);
            _in.ScheduleTypeList = await _event.GetScheduleType(value);
            List<SelectListItem> ddlList = new List<SelectListItem>();
            foreach (var item in _input)
            {
                SelectListItem _data = new SelectListItem();
                _data.Text = item.EventTypeName;
                _data.Value = item.EventTypeId.ToString();
                ddlList.Add(_data);
            }
            _in.ddlEventType = ddlList;

            List<SelectListItem> ddlListS = new List<SelectListItem>();
            foreach (var item in _inputS)
            {
                SelectListItem _data = new SelectListItem();
                _data.Text = item.SubTypeName;
                _data.Value = item.EventSubTypeId.ToString();
                ddlListS.Add(_data);
            }
            _in.ddlEventSubType = ddlListS;
            return PartialView(_in);
        }
        [Authorize(Policy = "Corporate")]
        public async Task<IActionResult> CoEventDetails(int id)
        {
            EventViewModel _in = new EventViewModel();
            var value = HttpContext.Session.GetString(SessionToken);
            if (string.IsNullOrEmpty(value))
            {
                string _token = await getToken();
                HttpContext.Session.SetString(SessionToken, _token);
                value = HttpContext.Session.GetString(SessionToken);
            }
            _in = await _event.GetCoEventDetailsWithEventId(value, id);

            return View(_in);
        }
        public async Task<PartialViewResult> _getVendorListForCorporate(int EID, int ESId, int EventDetailsId)
        {
            EventDetailsViewModel _eve = new EventDetailsViewModel();
            _eve.EventDetailsId = EventDetailsId;
            List<User> _in = new List<User>();
            var value = HttpContext.Session.GetString(SessionToken);
            if (string.IsNullOrEmpty(value))
            {
                string _token = await getToken();
                HttpContext.Session.SetString(SessionToken, _token);
                value = HttpContext.Session.GetString(SessionToken);
            }
            _in = await _event.GetVendorInterestUser(value, EID, ESId, EventDetailsId);
            _eve.UserListForAssign = new List<User>();
            _eve.UserListForAssign = _in;
            return PartialView(_eve);
        }

        [HttpPost]
        public async Task<ActionResult> CorporateApprovedVendor([FromBody] AssignVendorEvent _input)
        {
            List<VendorInvitation> _list = new List<VendorInvitation>();
            foreach (var item in _input.VIDetails)
            {
                VendorInvitation model = new VendorInvitation();
                model.VendorId = item.UserId;
                model.EventDetailsId = item.EventDetailsId;
                model.StatusId = 9;
                _list.Add(model);
            }
            var value = HttpContext.Session.GetString(SessionToken);
            if (string.IsNullOrEmpty(value))
            {
                string token = await getToken();
                HttpContext.Session.SetString(SessionToken, token);
                value = HttpContext.Session.GetString(SessionToken);
            }
            bool _out = await _event.AssingnVendorsToEvent(value, _list);
            if (_out == true)
            {

                return Json(_input.EventId);
            }

            return null;
        }
        #endregion

        #region Vendor
        [Authorize(Policy = "Vendor")]
        public async Task<IActionResult> VendorEventDetails(int id, int VId)
        {
            EventViewModel _in = new EventViewModel();
            var value = HttpContext.Session.GetString(SessionToken);
            if (string.IsNullOrEmpty(value))
            {
                string _token = await getToken();
                HttpContext.Session.SetString(SessionToken, _token);
                value = HttpContext.Session.GetString(SessionToken);
            }
            _in = await _event.GetVendorEventDetailsWithEventId(value, id, VId);
            _in.VendorId = VId;
            return View(_in);
        }

        public async Task<PartialViewResult> _getVendorAcceptForm()
        {
            return PartialView();
        }
        [HttpPost]
        public async Task<JsonResult> SaveVendorAcceptForm([FromBody] VendorInvitation model)
        {
            model.StatusId = 1;
            var value = HttpContext.Session.GetString(SessionToken);
            if (string.IsNullOrEmpty(value))
            {
                string token = await getToken();
                HttpContext.Session.SetString(SessionToken, token);
                value = HttpContext.Session.GetString(SessionToken);
            }
            bool _out = await _event.UpdateVendorsInvitationStatus(value, model);
            return Json(1);
        }
        #endregion



    }
}