using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using Xgame.Core;
using Xgame.Db;
using Xgame.Model;

namespace Xgame.Mvc
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
            services.AddOptions();
            services.AddDbContext<XgameContext>(o => o.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<XgameContext>()
              .AddDefaultTokenProviders();


            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    options.HttpOnly = HttpOnlyPolicy.Always;
            //    options.MinimumSameSitePolicy = SameSiteMode.Strict;
            //    options.Secure = CookieSecurePolicy.SameAsRequest;
            //}).ConfigureApplicationCookie(options => { 
            //    options.Cookie.Name = "Xgame";
            //    options.Cookie.Expiration = TimeSpan.FromDays(1);
            //    options.Cookie.HttpOnly = false;
            //    options.SlidingExpiration = true;
            //    options.LoginPath = new PathString("/Account/Login");
            //    options.LogoutPath = new PathString("/Account/Logout");
            //    options.AccessDeniedPath = new PathString("/AccessDenied");
            //})
            services.AddAuthentication(options =>
            {
               
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.ExpireTimeSpan = TimeSpan.FromHours(1);
                options.SlidingExpiration = true;
            });

            services.AddAuthentication();
            services.AddMvc();
            services.AddScoped<XgameContext>();
            services.AddScoped<SignInManager<AppUser>, MySignInManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");
            });
        }
    }
}
