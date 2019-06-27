using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Iway.Helper;
using Iway.Interface;
using Iway.Models.Helper;
using Iway.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace Iway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(o => o.LoginPath = new PathString("/account/login"));
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);//You can set Time   
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin",
                                  policy => policy.Requirements.Add(new AdminAcces("Admin")));
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Corporate",
                                  policy => policy.Requirements.Add(new AdminAcces("Corporate")));
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Vendor",
                                  policy => policy.Requirements.Add(new AdminAcces("Vendor")));
            });
            // existing code above
            services.AddSingleton<IAuthorizationHandler, AdminAccesHandler>();
            services.AddTransient<IToken, TokenRepo>();
            services.AddTransient<IEvent, EventRepo>();
            services.AddTransient<ILogin, LoginRepo>();
            services.AddTransient<IUser, UserRepo>();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });

            services.AddSingleton<RequestHandler>();
            services.AddHttpContextAccessor();
            services.AddDistributedMemoryCache();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession();

            app.UseStatusCodePagesWithReExecute("/Error", "?status={0}");


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        }


    }
}
