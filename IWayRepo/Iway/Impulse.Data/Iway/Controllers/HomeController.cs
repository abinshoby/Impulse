using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Iway.Models;
using Iway.Interface;
using Impulse.Model;
using Rotativa.AspNetCore;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.IO;

namespace Iway.Controllers
{
    public class HomeController : Controller
    {

        private readonly IToken _token;
     
        public const string SessionToken = "Token";

        public HomeController(IToken token)
        {
            this._token = token;
        }
        public async Task<string> getToken()
        {
            string tkn = await _token.GetToken();
            
            return tkn;

        }
        public async Task<ActionResult> Index()
        {
            var value = HttpContext.Session.GetString(SessionToken);
            if (string.IsNullOrEmpty(value))
            {
                string token = await getToken();
                HttpContext.Session.SetString(SessionToken, token);
                value = HttpContext.Session.GetString(SessionToken);
            }
            return View();
        }
        [HttpPost]
        [Route("Home/ToPdf")]
        public IActionResult ToPdf(PDF_ inp)
        {
            var root = "wwwroot/PDF_Data/";
            var pdfname =inp.Name+".pdf";
            var path = Path.Combine(root, pdfname);
            path = Path.GetFullPath(path);
            var report = new ViewAsPdf("PDF_template", inp)
            {

                PageMargins = { Left = 20, Bottom = 20, Right = 20, Top = 20 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                SaveOnServerPath = path,
                
            };
            
           return report;
           
        }
        [HttpPost]
        [Route("Home/Confirm_PDF_details")]
        public IActionResult Confirm_PDF_details(PDF_ inp)
        {
            var response = new HttpResponseMessage();
            string result_view = "<form method='post' asp-controller='Home' action='ToPdf'>Name: <input name='Name' readonly='true'  value='" + inp.Name+ "'/><br> Address: <input name='Address' readonly='true' value='" + inp.Address+ "'/><br> Amount: <input readonly='true'  name='Amount' value='" + inp.Amount+"'/><input type='submit' value='confirm'></form> ";
            
            
            ViewBag.result_view = result_view;

            return  View();

        }
        [Route("User/FormToPdf")]

        public IActionResult FormToPdf()
        {
            
            return View();
        }
        public IActionResult Login()
        { 
            return View();
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("User/SignUpWith")]
        public IActionResult SignUpWith()
        {
            return View("SignUpWith");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
