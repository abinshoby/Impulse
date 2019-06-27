using Iway.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Iway.Models.Helper
{
    public class CreateRoles
    {
        private readonly IUser _user;
        private readonly IToken _token;
        public CreateRoles(IToken token, IUser user)
        {
            this._token = token;
            this._user = user;
        }
        private async Task CreateRole(IServiceProvider serviceProvider)
        {
            //initializing custom roles   
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            string[] roleNames = { "Admin", "Corporate", "Vendor" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    //create the roles and seed them to the database: Question 1  
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            IdentityUser user = await UserManager.FindByEmailAsync("deepu.d@edifydata.com");

            if (user == null)
            {
                user = new IdentityUser()
                {
                    UserName = "Admin",
                    Email = "deepu.d@edifydata.com",
                };
                await UserManager.CreateAsync(user, "imp123");
            }
            await UserManager.AddToRoleAsync(user, "Admin");


            IdentityUser user1 = await UserManager.FindByEmailAsync("tejas@gmail.com");

            if (user1 == null)
            {
                user1 = new IdentityUser()
                {
                    UserName = "tejas@gmail.com",
                    Email = "tejas@gmail.com",
                };
                await UserManager.CreateAsync(user1, "Test@123");
            }
            await UserManager.AddToRoleAsync(user1, "User");

            IdentityUser user2 = await UserManager.FindByEmailAsync("rakesh@gmail.com");

            if (user2 == null)
            {
                user2 = new IdentityUser()
                {
                    UserName = "rakesh@gmail.com",
                    Email = "rakesh@gmail.com",
                };
                await UserManager.CreateAsync(user2, "Test@123");
            }
            await UserManager.AddToRoleAsync(user2, "HR");

        }
    }
}
