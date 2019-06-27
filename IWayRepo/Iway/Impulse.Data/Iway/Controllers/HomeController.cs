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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
