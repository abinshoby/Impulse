using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Impulse.Model;
using Iway.Helper;
using Iway.Interface;
using Iway.Models.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Iway.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        const string key = "E546C8DF278CD5931069B522E695D4F2";
        private readonly IToken _token;
        private readonly ILogin _login;
        private readonly IEvent _event;
        private readonly IUser _user;
        public const string SessionToken = "Token";
        public const string SessionUser = "User";
        public const string SessionUserType = "UserType";
        public DashboardController(IToken token, ILogin login, IEvent _event, IUser user)
        {
            this._token = token;
            this._login = login;
            this._event = _event;
            this._user = user;
        }
        public async Task<string> getToken()
        {
            string tkn = await _token.GetToken();
            return tkn;

        }


        #region Admin    

        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Index()
        {
            int UId = (int)HttpContext.Session.GetInt32(SessionUser);
            int UTID = (int)HttpContext.Session.GetInt32(SessionUserType);
            var value = HttpContext.Session.GetString(SessionToken);
            if (string.IsNullOrEmpty(value))
            {
                string token = await getToken();
                HttpContext.Session.SetString(SessionToken, token);
                value = HttpContext.Session.GetString(SessionToken);
            }
            User _in = new User();
            _in = await _login.GetUserDetails(value, UId);
            return View(_in);
        }
        [Authorize(Policy = "Admin")]
        public async Task<PartialViewResult> GetAdminEventList()
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
            return PartialView(_in);
        }

        #endregion

        #region Corporate
        [Authorize(Policy = "Corporate")]
        public async Task<IActionResult> CorporateIndex()
        {
            int UId = (int)HttpContext.Session.GetInt32(SessionUser);
            int UTID = (int)HttpContext.Session.GetInt32(SessionUserType);
            var value = HttpContext.Session.GetString(SessionToken);
            if (string.IsNullOrEmpty(value))
            {
                string token = await getToken();
                HttpContext.Session.SetString(SessionToken, token);
                value = HttpContext.Session.GetString(SessionToken);
            }

            User _in = await _login.GetUserDetails(value, UId);
            return View(_in);
        }




        [Authorize(Policy = "Corporate")]

        public async Task<PartialViewResult> GetCorporateEventList(int Uid)
        {

            List<EventViewModel> _in = new List<EventViewModel>();
            var value = HttpContext.Session.GetString(SessionToken);
            if (string.IsNullOrEmpty(value))
            {
                string _token = await getToken();
                HttpContext.Session.SetString(SessionToken, _token);
                value = HttpContext.Session.GetString(SessionToken);
            }
            _in = await _event.GetCorporateEventList(value, Uid);

            return PartialView(_in);
        }
        #endregion

        #region Vendor
        [Authorize(Policy = "Vendor")]
        public async Task<IActionResult> VendorIndex()
        {
            int UId = (int)HttpContext.Session.GetInt32(SessionUser);
            int UTID = (int)HttpContext.Session.GetInt32(SessionUserType);
            var value = HttpContext.Session.GetString(SessionToken);
            if (string.IsNullOrEmpty(value))
            {
                string token = await getToken();
                HttpContext.Session.SetString(SessionToken, token);
                value = HttpContext.Session.GetString(SessionToken);
            }
            User _in = await _login.GetUserDetails(value, UId);
            return View(_in);
        }
        [Authorize(Policy = "Vendor")]
        public async Task<PartialViewResult> GetVendorEventList(int Uid)
        {

            List<EventViewModel> _in = new List<EventViewModel>();
            var value = HttpContext.Session.GetString(SessionToken);
            if (string.IsNullOrEmpty(value))
            {
                string _token = await getToken();
                HttpContext.Session.SetString(SessionToken, _token);
                value = HttpContext.Session.GetString(SessionToken);
            }
            _in = await _event.GetVendorEventList(value, Uid);
            _in = _in.GroupBy(m => m.EventId).Select(g => g.First()).ToList();
            ViewBag.UserId = Uid;
            return PartialView(_in);
        }
        [Authorize(Policy = "Vendor")]
        public async Task<PartialViewResult> GetVendorProfileCompletion(int Uid)
        {
            VendorInterest _input = new VendorInterest();
            _input.UserId = Uid;
            List<VendorInterest> _in = new List<VendorInterest>();
            List<EventType> _inputET = new List<EventType>();
            List<EventSubTypeModel> _inputEST = new List<EventSubTypeModel>();
            var value = HttpContext.Session.GetString(SessionToken);
            if (string.IsNullOrEmpty(value))
            {
                string _token = await getToken();
                HttpContext.Session.SetString(SessionToken, _token);
                value = HttpContext.Session.GetString(SessionToken);
            }
            _in = await _user.GetVendorInterestByUser(value, Uid);

            _inputET = await _event.GetEventType(value);
            _inputEST = await _event.GetEventSubType(value, _inputET.FirstOrDefault().EventTypeId);

            List<SelectListItem> ddlList = new List<SelectListItem>();
            foreach (var item in _inputET)
            {
                SelectListItem _data = new SelectListItem();
                _data.Text = item.EventTypeName;
                _data.Value = item.EventTypeId.ToString();
                ddlList.Add(_data);
            }
            _input.ddlEventType = ddlList;

            List<SelectListItem> ddlListS = new List<SelectListItem>();
            foreach (var item in _inputEST)
            {
                SelectListItem _data = new SelectListItem();
                _data.Text = item.SubTypeName;
                _data.Value = item.EventSubTypeId.ToString();
                ddlListS.Add(_data);
            }
            _input.ddlEventSubType = ddlListS;
            if (_in != null && _in.Count > 0)
            {
                return null;
            }
            else
            {
                return PartialView(_input);
            }
        }
        [HttpPost]
        public async Task<ActionResult> AddVendorInterest(VendorInterest _input)
        {
            List<VendorInterest> _list = new List<VendorInterest>();
            if (_input.EventSubTypeList.Count() > 0)
            {
                foreach (int item in _input.EventSubTypeList)
                {
                    VendorInterest model = new VendorInterest();
                    model.EventTypeId = _input.EventTypeId;
                    model.EventSubTypeId = item;
                    model.Place = _input.Place;
                    model.UserId = _input.UserId;
                    _list.Add(model);
                }
            }
            var value = HttpContext.Session.GetString(SessionToken);
            if (string.IsNullOrEmpty(value))
            {
                string token = await getToken();
                HttpContext.Session.SetString(SessionToken, token);
                value = HttpContext.Session.GetString(SessionToken);
            }
            bool _out = await _event.PostVendorInterest(value, _list);
            if (_out == true)
            {
                return RedirectToAction("VendorIndex", "Dashboard", new { @id = _input.UserId });

            }
            return null;
        }
        [Authorize(Policy = "Vendor")]
        public async Task<PartialViewResult> _addMoreVendorInterest(int Uid)
        {
            VendorInterest _input = new VendorInterest();
            _input.UserId = Uid;
            List<EventType> _inputET = new List<EventType>();
            List<EventSubTypeModel> _inputEST = new List<EventSubTypeModel>();
            var value = HttpContext.Session.GetString(SessionToken);
            if (string.IsNullOrEmpty(value))
            {
                string _token = await getToken();
                HttpContext.Session.SetString(SessionToken, _token);
                value = HttpContext.Session.GetString(SessionToken);
            }


            _inputET = await _event.GetEventType(value);
            _inputEST = await _event.GetEventSubType(value, _inputET.FirstOrDefault().EventTypeId);

            List<SelectListItem> ddlList = new List<SelectListItem>();
            foreach (var item in _inputET)
            {
                SelectListItem _data = new SelectListItem();
                _data.Text = item.EventTypeName;
                _data.Value = item.EventTypeId.ToString();
                ddlList.Add(_data);
            }
            _input.ddlEventType = ddlList;

            List<SelectListItem> ddlListS = new List<SelectListItem>();
            foreach (var item in _inputEST)
            {
                SelectListItem _data = new SelectListItem();
                _data.Text = item.SubTypeName;
                _data.Value = item.EventSubTypeId.ToString();
                ddlListS.Add(_data);
            }
            _input.ddlEventSubType = ddlListS;
            return PartialView(_input);

        }
        [Authorize(Policy = "Vendor")]
        public async Task<PartialViewResult> GetVendorInterest(int Uid)
        {

            List<VendorInterest> _in = new List<VendorInterest>();
            var value = HttpContext.Session.GetString(SessionToken);
            if (string.IsNullOrEmpty(value))
            {
                string _token = await getToken();
                HttpContext.Session.SetString(SessionToken, _token);
                value = HttpContext.Session.GetString(SessionToken);
            }
            _in = await _user.GetVendorInterestByUser(value, Uid);
            ViewBag.UserId = Uid;
            return PartialView(_in);
        }
        #endregion

        #region Citizen
        public async Task<IActionResult> CitizenIndex()
        {
            int UId = (int)HttpContext.Session.GetInt32(SessionUser);
            int UTID = (int)HttpContext.Session.GetInt32(SessionUserType);
            var value = HttpContext.Session.GetString(SessionToken);
            if (string.IsNullOrEmpty(value))
            {
                string token = await getToken();
                HttpContext.Session.SetString(SessionToken, token);
                value = HttpContext.Session.GetString(SessionToken);
            }
            User _in = await _login.GetUserDetails(value, UId);
            return View(_in);
        }
        public async Task<PartialViewResult> loadCitizenProfile(int Uid)
        {
            Citizen _input = new Citizen();

            var value = HttpContext.Session.GetString(SessionToken);
            if (string.IsNullOrEmpty(value))
            {
                string _token = await getToken();
                HttpContext.Session.SetString(SessionToken, _token);
                value = HttpContext.Session.GetString(SessionToken);
            }
            _input = await _user.GetCitizenDetails(value, Uid);

            if (_input.UserId == 0) { _input.UserId = Uid; }


            return PartialView(_input);
        }

        [HttpPost]
        public async Task<ActionResult> AddCitizenProfile(Citizen _input)
        {

            var value = HttpContext.Session.GetString(SessionToken);
            if (string.IsNullOrEmpty(value))
            {
                string token = await getToken();
                HttpContext.Session.SetString(SessionToken, token);
                value = HttpContext.Session.GetString(SessionToken);
            }
            bool _out = await _user.PostCitizenDetails(value, _input);
            if (_out == true)
            {
                return RedirectToAction("CitizenIndex", "Dashboard", new { @id = _input.UserId });

            }
            return null;
        }
        #endregion




    }
}