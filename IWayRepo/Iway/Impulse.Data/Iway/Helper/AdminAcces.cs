using Impulse.Model;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Iway.Helper
{

    public class AdminAcces : IAuthorizationRequirement
    {
        public AdminAcces(string userType)
        {
            UserType = userType;
        }

        public string UserType { get; set; }
    }
    public class AdminAccesHandler : AuthorizationHandler<AdminAcces>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminAcces requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.Role))
            {
                return Task.CompletedTask;
            }

            string role = context.User.FindFirst(c => c.Type == ClaimTypes.Role).Value.ToString();
            if (role == requirement.UserType)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
        
    }
}
