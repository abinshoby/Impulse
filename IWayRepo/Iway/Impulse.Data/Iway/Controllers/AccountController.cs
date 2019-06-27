using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Iway.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login( string returnUrl)
        {
            return RedirectToAction("Login", "User");
        }
    }
}