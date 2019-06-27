using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Iway.Models.Helper
{
    public class RequestHandler
    {
        IHttpContextAccessor _httpContextAccessor;
        public RequestHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        internal void HandleAboutRequest()
        {
            // handle the request  
            var message = "The HttpContextAccessor seems to be working!!";
            _httpContextAccessor.HttpContext.Session.SetString("message", message);
        }
    }
}
