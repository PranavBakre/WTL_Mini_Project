using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackUniverse.Models;
using HackUniverse.Models.Hackathon_User_Interactions;
using HackUniverse.Models.ProblemStatements;
using HackUniverse.Models.User;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HackUniverse
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
            services.AddControllersWithViews();
            services.Add(new ServiceDescriptor(typeof(HackathonContext),
                new HackathonContext(Configuration.GetConnectionString("DefaultConnection"))));
            services.Add(new ServiceDescriptor(typeof(ProblemStatementContext),
                new ProblemStatementContext(Configuration.GetConnectionString("DefaultConnection"))));
            services.Add(new ServiceDescriptor(typeof(UserContext),
                new UserContext(Configuration.GetConnectionString("DefaultConnection"))));
            services.Add(new ServiceDescriptor(typeof(Hackathon_UserContext),
                new Hackathon_UserContext(Configuration.GetConnectionString("DefaultConnection"))));
            services.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(30));
            services.Configure<CookiePolicyOptions>(options => options.CheckConsentNeeded = context=> false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
