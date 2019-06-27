using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Impulse.Model;
using Iway.Helper;
using Iway.Interface;
using Iway.Models.Helper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Iway.Controllers
{

    public class UserController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        const string key = "E546C8DF278CD5931069B522E695D4F2";
        private readonly IToken _token;
        private readonly ILogin _login;
        private readonly IUser _user;
        public const string SessionToken = "Token";
        public const string SessionUser = "User";
        public const string SessionUserType = "UserType";
        private IHostingEnvironment _hostingEnvironment;

        public UserController(IToken token, ILogin login, IUser user, IHttpContextAccessor httpContextAccessor, IHostingEnvironment hostingEnvironment)
        {
            this._token = token;
            this._login = login;
            this._user = user;
            _httpContextAccessor = httpContextAccessor;
            _hostingEnvironment = hostingEnvironment;
        }
        #region Token
        public async Task<string> getToken()
        {
            string tkn = await _token.GetToken();
            return tkn;

        }

        #endregion
        #region Common
        public async Task<IActionResult> Profile()
        {
            int UId = (int)HttpContext.Session.GetInt32(SessionUser);
            User _in = new User();
            var value = HttpContext.Session.GetString(SessionToken);
            if (string.IsNullOrEmpty(value))
            {
                string _token = await getToken();
                HttpContext.Session.SetString(SessionToken, _token);
                value = HttpContext.Session.GetString(SessionToken);
            }
            _in = await _login.GetUserDetails(value, UId);
            return View(_in);
        }
        #endregion
        #region Login
        public IActionResult Login()
        {
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login _input)
        {
            if (ModelState.IsValid)
            {
                var value = HttpContext.Session.GetString(SessionToken);
                if (string.IsNullOrEmpty(value))
                {
                    string token = await getToken();
                    HttpContext.Session.SetString(SessionToken, token);
                    value = HttpContext.Session.GetString(SessionToken);
                }
                User _out = await _login.Login(value, _input);
                if (_out != null)
                {
                    var claims = new[] { new Claim(ClaimTypes.UserData, _out.UserId.ToString()), new Claim(ClaimTypes.Role, _out.LookUpUserType.UserType), new Claim(ClaimTypes.PrimarySid, _out.LookUpUserTypeId.ToString()) };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var tempData = _out;
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(identity));
                    HttpContext.Session.SetInt32(SessionUser, (int)_out.UserId);
                    HttpContext.Session.SetInt32(SessionUserType, (int)_out.LookUpUserTypeId);
                    if (_out.LookUpUserTypeId == 3)
                    {
                        return RedirectToAction("Index", "Dashboard");
                    }
                    else if (_out.LookUpUserTypeId == 1)
                    {
                        return RedirectToAction("CorporateIndex", "Dashboard");
                    }
                    else if (_out.LookUpUserTypeId == 2)
                    {
                        return RedirectToAction("VendorIndex", "Dashboard");
                    }
                    else if (_out.LookUpUserTypeId == 4)
                    {
                        return RedirectToAction("CitizenIndex", "Dashboard");
                    }
                }
                else
                {
                    ModelState.AddModelError("loginError", "Invalid Username Or Password");
                }
            }

            return View();
        }
        #endregion

        #region Corporate

        public IActionResult CorporateSignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CorporateSignUp([FromForm]SignUp _input)
        {
            if (ModelState.IsValid)
            {
                bool status = false;
                var value = HttpContext.Session.GetString(SessionToken);
                if (string.IsNullOrEmpty(value))
                {
                    string token = await getToken();
                    HttpContext.Session.SetString(SessionToken, token);
                    value = HttpContext.Session.GetString(SessionToken);
                }
                SignUpHelper helper = new SignUpHelper();
                User _in = await helper.GetUserInput(_input);
                User _out = await _login.AddUserDetails(value, _in);
                if (_out != null)
                {
                    status = await _login.Mail(value, _in); var claims = new[] { new Claim(ClaimTypes.UserData, _out.UserId.ToString()), new Claim(ClaimTypes.Role, _out.LookUpUserType.UserType), new Claim(ClaimTypes.PrimarySid, _out.LookUpUserTypeId.ToString()) };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var tempData = _out;
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(identity));
                    HttpContext.Session.SetInt32(SessionUser, (int)_out.UserId);
                    HttpContext.Session.SetInt32(SessionUserType, (int)_out.LookUpUserTypeId);
                    //ModelState.AddModelError("successMsg", "Welcome " + _in.Team.TeamName + "!! . Thanks for signing up for iWAY! We're excited to have you onboard.");
                    return RedirectToAction("CorporateIndex", "Dashboard");
                }

            }
            return View();
        }
        [Authorize]
        public async Task<IActionResult> UserCorporate()
        {
            var value = HttpContext.Session.GetString(SessionToken);
            if (string.IsNullOrEmpty(value))
            {
                string token = await getToken();
                HttpContext.Session.SetString(SessionToken, token);
                value = HttpContext.Session.GetString(SessionToken);
            }
            List<User> _in = await _user.GetUserListByType(value, 1);
            return View(_in);

        }
        #endregion


        #region Vendor

        public async Task<PartialViewResult> _getVendorInterestDetails(int VId)
        {
            VendorInterest _eve = new VendorInterest();
            var value = HttpContext.Session.GetString(SessionToken);
            if (string.IsNullOrEmpty(value))
            {
                string _token = await getToken();
                HttpContext.Session.SetString(SessionToken, _token);
                value = HttpContext.Session.GetString(SessionToken);
            }
            _eve = await _user.GetVendorInterestDetails(value, VId);
            return PartialView(_eve);
        }
        [HttpPost]
        public async Task<ActionResult> PutVendorInterest(VendorInterest _input)
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
            bool _out = await _user.PutVendorInterest(value, _list);
            if (_out == true)
            {
                return RedirectToAction("Profile", "User");

            }
            return null;
        }
        public IActionResult VendorSignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> VendorSignUp([FromForm] SignUp _input)
        {
            if (ModelState.IsValid)
            {
                bool status = false;
                var value = HttpContext.Session.GetString(SessionToken);
                if (string.IsNullOrEmpty(value))
                {
                    string token = await getToken();
                    HttpContext.Session.SetString(SessionToken, token);
                    value = HttpContext.Session.GetString(SessionToken);
                }
                SignUpHelper helper = new SignUpHelper();
                User _in = await helper.GetVendorUserInput(_input);
                User _out = await _login.AddUserDetails(value, _in);
                if (_out != null)
                {
                    status = await _login.VendorMail(value, _in);
                    ModelState.AddModelError("successMsg", "Welcome " + _in.Team.TeamName + "!! . Thanks for signing up for iWAY! We're excited to have you onboard.");


                }

            }

            return View();
        }
        [Authorize]
        public async Task<IActionResult> UserPending()
        {
            var value = HttpContext.Session.GetString(SessionToken);
            if (string.IsNullOrEmpty(value))
            {
                string token = await getToken();
                HttpContext.Session.SetString(SessionToken, token);
                value = HttpContext.Session.GetString(SessionToken);
            }
            List<User> _in = await _user.GetPendingList(value);
            return View(_in);

        }
        [Authorize]
        public async Task<IActionResult> UserApproval()
        {
            var value = HttpContext.Session.GetString(SessionToken);
            if (string.IsNullOrEmpty(value))
            {
                string token = await getToken();
                HttpContext.Session.SetString(SessionToken, token);
                value = HttpContext.Session.GetString(SessionToken);
            }
            List<User> _in = await _user.GetUserListByType(value, 2);
            return View(_in);

        }

        [HttpPost]
        [Authorize]
        public async Task<JsonResult> ApproveVendor(int id)
        {

            var value = HttpContext.Session.GetString(SessionToken);
            if (string.IsNullOrEmpty(value))
            {
                string token = await getToken();
                HttpContext.Session.SetString(SessionToken, token);
                value = HttpContext.Session.GetString(SessionToken);
            }
            User _in = new User();
            _in.UserId = id;
            _in.LookUpUserStatusId = 1;
            User _out = await _user.UpadtePendingListStatus(value, _in);
            return Json(_out);
        }
        #endregion


        #region ICitizen


        public IActionResult ICitizenSignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ICitizenSignUp([FromForm] SignUp _input)
        {
            if (ModelState.IsValid)
            {
                bool status = false;
                var value = HttpContext.Session.GetString(SessionToken);
                if (string.IsNullOrEmpty(value))
                {
                    string token = await getToken();
                    HttpContext.Session.SetString(SessionToken, token);
                    value = HttpContext.Session.GetString(SessionToken);
                }
                SignUpHelper helper = new SignUpHelper();
                User _in = await helper.GetCitizenUserInput(_input);
                User _out = await _login.AddUserDetails(value, _in);
                if (_out != null)
                {
                    status = await _login.VendorMail(value, _in);
                    ModelState.AddModelError("successMsg", "Welcome " + _in.Team.TeamName + "!! . Thanks for signing up for iWAY! We're excited to have you onboard.");


                }

            }

            return View();
        }

        #endregion

        #region Error
        [HttpGet("/Error"), HttpPost("/Error")]
        public IActionResult Error([FromQuery] int status)
        {
            return View();
        }
        #endregion

        public async Task<IActionResult> UserICitizen()
        {
            var value = HttpContext.Session.GetString(SessionToken);
            if (string.IsNullOrEmpty(value))
            {
                string token = await getToken();
                HttpContext.Session.SetString(SessionToken, token);
                value = HttpContext.Session.GetString(SessionToken);
            }
            List<User> _in = await _user.GetUserListByType(value, 4);
            return View(_in);

        }
        public async Task<IActionResult> ICitizenDetails(int UID)
        {
            Citizen _input = new Citizen();

            var value = HttpContext.Session.GetString(SessionToken);
            if (string.IsNullOrEmpty(value))
            {
                string _token = await getToken();
                HttpContext.Session.SetString(SessionToken, _token);
                value = HttpContext.Session.GetString(SessionToken);
            }
            _input = await _user.GetCitizenDetails(value, UID);
            return View(_input);

        }
        public ActionResult OnPostImport()
        {
            return View();
        }
        public class test
        {
            public int userId { get; set; }
        }
        [HttpPost]
        public async Task<ActionResult> OnPostImporttest([FromForm] test _test)
        {
            var value = HttpContext.Session.GetString(SessionToken);
            if (string.IsNullOrEmpty(value))
            {
                string token = await getToken();
                HttpContext.Session.SetString(SessionToken, token);
                value = HttpContext.Session.GetString(SessionToken);
            }
            IFormFile file = Request.Form.Files[0];
            string folderName = "Upload";
            string webRootPath = _hostingEnvironment.WebRootPath;
            string newPath = Path.Combine(webRootPath, folderName);
            StringBuilder sb = new StringBuilder();
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            if (file.Length > 0)
            {
                string sFileExtension = Path.GetExtension(file.FileName).ToLower();
                ISheet sheet;
                string fullPath = Path.Combine(newPath, file.FileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                    stream.Position = 0;
                    if (sFileExtension == ".xls")
                    {
                        HSSFWorkbook hssfwb = new HSSFWorkbook(stream); //This will read the Excel 97-2000 formats  
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook  
                    }
                    else
                    {
                        XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format  
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   
                    }
                    IRow headerRow = sheet.GetRow(0); //Get Header Row
                    int cellCount = headerRow.LastCellNum;
                    sb.Append("<table class='table'><tr>");
                    for (int j = 0; j < cellCount; j++)
                    {
                        NPOI.SS.UserModel.ICell cell = headerRow.GetCell(j);
                        if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                        sb.Append("<th>" + cell.ToString() + "</th>");
                    }
                    sb.Append("</tr>");
                    sb.AppendLine("<tr>");
                    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File
                    {
                        User _in = new User();
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue;
                        if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                        _in.Team = new Team();
                        _in.Contact = new List<Contact>();
                        _in.UserName = row.GetCell(4).ToString();
                        _in.Password = "imp123";
                        _in.LookUpUserTypeId = 4;
                        _in.LookUpUserStatusId = 1;
                        _in.RecordStatus = "Active";

                        Team _inTeam = new Team();
                        _inTeam.TeamName = row.GetCell(1).ToString();
                        _inTeam.TeamDescription = null;
                        _inTeam.TeamLogo = null;
                        _in.Team = _inTeam;
                        List<Contact> _inConL = new List<Contact>();
                        Contact _inCon = new Contact();
                        _inCon.LookUpContactTypeId = 1;
                        _inCon.Value = row.GetCell(4).ToString();
                        _inConL.Add(_inCon);
                        Contact _inConP = new Contact();
                        _inConP.LookUpContactTypeId = 2;
                        _inConP.Value = row.GetCell(5).ToString();
                        _inConL.Add(_inConP);
                        _in.Contact = _inConL;
                        User _out = await _login.AddUserDetails(value, _in);
                        Citizen _citizen = new Citizen();
                        _citizen.UserId = (int)_out.UserId;
                        _citizen.CaptainName = row.GetCell(0).ToString();
                        _citizen.PlayerName = row.GetCell(1).ToString();
                        _citizen.CompanyName = row.GetCell(2).ToString();
                        _citizen.Designation = row.GetCell(3).ToString();
                        _citizen.DOB = Convert.ToDateTime(row.GetCell(6).ToString());

                        _citizen.BloodGroup = row.GetCell(7).ToString();
                        _citizen.HREmail = row.GetCell(8).ToString();
                        _citizen.HRPhone = row.GetCell(9).ToString();
                        _citizen.CompanyEmail = row.GetCell(4).ToString();

                        bool _outData = await _user.PostCitizenDetails(value, _citizen);
                        //sb.Append("<td>" + row.GetCell(j).ToString() + "</td>");

                        sb.AppendLine("</tr>");
                    }
                    sb.Append("</table>");
                }
            }
            return this.Content(sb.ToString());
        }
    }
}