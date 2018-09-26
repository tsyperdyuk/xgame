using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xgame.Db;
using Xgame.Db.Entities;

namespace WebApplication
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
            var connationString = @"Server = (localdb)\mssqllocaldb; Database = XgameDB; Trusted_Connection = True;";
            services.AddDbContext<XgameContext>(o => o.UseSqlServer(connationString));
            // services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<XgameContext>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, XgameContext xgameContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            //app.UseStaticFiles();
            //app.UseAuthentication();
            app.UseMvc();
        }
    }
}
