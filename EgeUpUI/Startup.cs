using BussinesLayer.Abstrack;
using BussinesLayer.Concrete;
using Hangfire;
using DataaccsessLayer.Abstract;
using DataaccsessLayer.Concrete;
using DataaccsessLayer.EntitiyFreamWork;
using DataaccsessLayer.Table;

using EgeUpUI.Controllers;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EgeUp.BackgroundJob.Managers.Schedules;
using EgeUpBackgroundJob.Managers.Schedules;
using Hangfire.SqlServer;

namespace EgeUpUI
{
    public class Startup
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Startup(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Context>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
   .AddTokenProvider<DataProtectorTokenProvider<AppUser>>(TokenOptions.DefaultProvider).AddEntityFrameworkStores<Context>()
    .AddRoles<AppRole>()
    .AddRoleManager<RoleManager<AppRole>>()
    .AddDefaultTokenProviders();

            services.AddHangfire(x =>
            {
                var option = new SqlServerStorageOptions
                {
                    PrepareSchemaIfNecessary = true,
                    QueuePollInterval = TimeSpan.FromMinutes(5),
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    UseRecommendedIsolationLevel = true,
                    UsePageLocksOnDequeue = true,
                    DisableGlobalLocks = true
                };

                x.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection"), option).WithJobExpirationTimeout(TimeSpan.FromHours(6));
            });
            services.AddHangfireServer();
            services.AddControllersWithViews().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Startup>());
            services.AddScoped<IWebSiteDal, EFWebsiteDal>();
            services.AddScoped<IMailDal, EFMailDal>();
            services.AddScoped<IMailService, EFMail>();
            services.AddScoped<IWebsiteService, EFWebsiteUrl>();
            services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromDays(5);
                options.SlidingExpiration = true;
            });


        }


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
            app.UseStatusCodePagesWithReExecute("/ErrorPage/Error", "?code={0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseHangfireDashboard("/hangfire");
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "areas",
                   pattern: "{area:exists}/{controller=Login}/{action=Index}/{id?}"
               );
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "error",
                    pattern: "/ErrorPage/Error/{code}",
                    defaults: new { controller = "ErrorPage", action = "Error" }
                );


            });



            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<Context>();
                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
                var logger = services.GetRequiredService<ILogger<Startup>>();

                try
                {
                    context.Database.Migrate();
                    RecurringJobs.DatabaseBackupOperation();
                    CheckWebsiteJob.DatabaseBackupOperation();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Veritabaný migrasyonu veya diðer iþlemler sýrasýnda bir hata oluþtu.");
                }
                CreateDefaultRoles(roleManager, "Admin");

                CreateDefaultRoles(roleManager, "Musteri");
                if (!context.Companies.Any())
                {
                    var company = new Company
                    {
                       
                        CompanyName = "Örnek_Sirket",
                        Statues=false
                    
                    };

                    context.Companies.Add(company);
                    context.SaveChanges();
                }
                if (!context.Users.Any())
                {
                    var user = new AppUser
                    {
                        UserName = "EgeUp",
                        Name = "EgeUp",
                        Email = "EgeUp@Test.com",
                        ReferanceCode = 123456,
                        Companyid = 1,
                        EmailConfirmed = true
                    };

                    var password = "EgeUp123";

                    if (userManager.FindByNameAsync(user.UserName).Result == null)
                    {
                        var result = userManager.CreateAsync(user, password).Result;

                        if (result.Succeeded)
                        {
                            userManager.AddToRoleAsync(user, "Admin").Wait();
                        }
                        else
                        {
                            foreach (var error in result.Errors)
                            {
                                logger.LogError("Kullanýcý oluþturma hatasý: " + error.Description);
                            }
                        }
                    }
                }

            }


        }
        private void CreateDefaultRoles(RoleManager<AppRole> roleManager, string roleName)
        {
            if (!roleManager.RoleExistsAsync(roleName).Result)
            {
                var role = new AppRole
                {
                    Name = roleName
                };
                var roleResult = roleManager.CreateAsync(role).Result;

                if (!roleResult.Succeeded)
                {

                    foreach (var error in roleResult.Errors)
                    {
                    }
                }
            }

        }
    }
}
