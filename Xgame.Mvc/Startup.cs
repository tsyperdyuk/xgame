using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xgame.Core;
using Xgame.Db;
using Xgame.Db.Entities;
using Xgame.Model;
using Xgame.Model.QuestionModel;

namespace Xgame.Mvc
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IConfiguration configuration)
        {
            Configuration = configuration;
        }     

        public IConfiguration Configuration { get; set; }

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

                    options.ClaimsIdentity.RoleClaimType = UserClaimTypes.Roles;
                    options.ClaimsIdentity.UserIdClaimType = UserClaimTypes.UserId;
                    options.ClaimsIdentity.UserNameClaimType = UserClaimTypes.UserName;
                })
                .AddEntityFrameworkStores<XgameContext>()
                .AddSignInManager<MySignInManager>()
                .AddDefaultTokenProviders();

            services
                .AddAuthentication(options =>
                {
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.LoginPath = "/Account/Login";
                    options.LogoutPath = "/Account/Logout";
                    options.ExpireTimeSpan = TimeSpan.FromHours(1);
                    options.SlidingExpiration = true;
                });
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddScoped<XgameContext>();
            services.AddScoped<SignInManager<AppUser>, MySignInManager>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<StartInitializer>();

            ConfigAutoMapper();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            
            app.UseMvcWithDefaultRoute();            
        }

        private void ConfigAutoMapper()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Question, QuestionRepresentModel>()
                    .ForMember("UserName", opt => opt.MapFrom(c => c.User.UserName));
                cfg.CreateMap<Question, QuestionCreateModel>();
                cfg.CreateMap<Question, QuestionReviewModel>();
                cfg.CreateMap<Question, QuestionUpdateModel>();
                cfg.CreateMap<Question, QuestionRejectModel>();
                cfg.CreateMap<QuestionUpdateModel, Question>();
                cfg.CreateMap<QuestionUpdateModel, Question>();
                cfg.CreateMap<Question, QuestionUpdateModel>();
                cfg.CreateMap<QuestionCreateModel, Question>();
            });
        }
    }
}
