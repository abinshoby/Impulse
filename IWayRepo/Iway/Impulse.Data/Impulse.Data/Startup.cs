using System;
using System.Text;
using Impulse.Data.BLL.EF;
using Impulse.Data.BLL.Handler;
using Impulse.Data.BLL.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Impulse.Data
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
         .AddJwtBearer(options =>
         {
             options.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidateIssuer = true,
                 ValidateAudience = true,
                 ValidateLifetime = true,
                 ValidateIssuerSigningKey = true,
                 ValidIssuer = Configuration["Logging:Jwt:Issuer"],
                 ValidAudience = Configuration["Logging:Jwt:Issuer"],
                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Logging:Jwt:Key"]))
             };
         });
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials()
                .Build());
            });
            //var connection = @"Server=DESKTOP-SNKULO6;Database=impServer;User Id=edify;Password=edify12";
            //services.AddDbContext<DataContext>(options => options.UseMySQL(connection));
            services.AddDbContextPool<DataContext>((serviceProvider, optionsBuilder) =>
            {
                optionsBuilder.UseMySql(Configuration["Logging:ConnectionStrings:DefaultConnection"],

                     mysqlOptions =>
                     {
                         mysqlOptions.ServerVersion(new Version(5, 7, 17), ServerType.MySql); // replace with your Server Version and Type
                     }
                    );
                optionsBuilder.UseInternalServiceProvider(serviceProvider);
            });

            services.AddTransient<IUser, UserHandler>();
            services.AddTransient<ICitizen, CitizenHandler>();
            services.AddTransient<IEvent, BLL.Handler.EventHandler>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddEntityFrameworkMySql();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
