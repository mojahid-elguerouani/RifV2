using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FasDemo.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FasDemo.Services.Security;
using FasDemo.Models;
using FasDemo.Hubs;
using Microsoft.AspNetCore.Http.Features;

namespace FasDemo
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

            services.Configure<FormOptions>(options => options.MultipartBodyLengthLimit = long.MaxValue); // or other given limit


            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
                options.HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            /// Get Custom Identity Default Options
            IConfigurationSection identityDefaultOptionsConfigurationSection = Configuration.GetSection("IdentityDefaultOptions");

            services.Configure<IdentityDefaultOptions>(identityDefaultOptionsConfigurationSection);

            var identityDefaultOptions = identityDefaultOptionsConfigurationSection.Get<IdentityDefaultOptions>();
            //services.AddDefaultIdentity<ApplicationUser>().AddDefaultUI(UIFramework.Bootstrap4);
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // Password settings
                options.Password.RequireDigit = identityDefaultOptions.PasswordRequireDigit;
                options.Password.RequiredLength = identityDefaultOptions.PasswordRequiredLength;
                options.Password.RequireNonAlphanumeric = identityDefaultOptions.PasswordRequireNonAlphanumeric;
                options.Password.RequireUppercase = identityDefaultOptions.PasswordRequireUppercase;
                options.Password.RequireLowercase = identityDefaultOptions.PasswordRequireLowercase;
                options.Password.RequiredUniqueChars = identityDefaultOptions.PasswordRequiredUniqueChars;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(identityDefaultOptions.LockoutDefaultLockoutTimeSpanInMinutes);
                options.Lockout.MaxFailedAccessAttempts = identityDefaultOptions.LockoutMaxFailedAccessAttempts;
                options.Lockout.AllowedForNewUsers = identityDefaultOptions.LockoutAllowedForNewUsers;

                // User settings
                options.User.RequireUniqueEmail = identityDefaultOptions.UserRequireUniqueEmail;

                // email confirmation require
                options.SignIn.RequireConfirmedEmail = identityDefaultOptions.SignInRequireConfirmedEmail;
            })
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();


            // cookie settings
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = identityDefaultOptions.CookieHttpOnly;
                options.Cookie.Expiration = TimeSpan.FromDays(identityDefaultOptions.CookieExpiration);
                options.SlidingExpiration = identityDefaultOptions.SlidingExpiration;
            });


            /// Get Custom Super Admin Default options
            services.Configure<SuperAdminDefaultOptions>(Configuration.GetSection("SuperAdminDefaultOptions"));

            /// transient service 
            /// desc: Transient objects are always different; a new instance is provided to every controller and every service
            /// Add Custom Common Security Service as transient service
            services.AddTransient<Services.Security.ICommon, Services.Security.Common>();
            /// Add Custom Log service as transient service
            services.AddTransient<Services.Log.IRepository, Services.Log.Repository>();
            /// Add Custom common app service as transient service
            services.AddTransient<Services.App.ICommon, Services.App.Common>();

            /// scoped service 
            /// Add Custom Common Database servcie as scoped service
            services.AddScoped<Services.Database.ICommon, Services.Database.Common>();
            
            services.AddCors();

            services.AddSignalR();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            Services.Database.ICommon dbInit)
        {
            //custom exception handling, to catch 404
            app.Use(async (context, next) =>
            {
                await next();

                if (context.Response.StatusCode == 404 && !context.Response.HasStarted)
                {
                    string originalPath = context.Request.Path.Value;
                    context.Items["originalPath"] = originalPath;
                    context.Request.Path = "/Error/404";
                    await next();
                }
            });
             
            app.UseExceptionHandler("/Error/500");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            //}

            //init database with custom seed data
            dbInit.Initialize().Wait();

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCors(x => x.AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials()
               .AllowAnyOrigin()
               .SetIsOriginAllowed(_ => true)
               //.WithOrigins("https://localhost:4200")
               );

            app.UseAuthentication();

            //app.UseSignalR(route =>
            //{
            //    route.MapHub<ChatHub>("/Home/Index");
            //});
            app.UseSignalR(routes =>
            {

                routes.MapHub<ChatHub>("/chathub");
               // routes.MapHub<ChatHub>("hubs/chat");
                //routes.MapHub<MessageHub>("hubs/message");
            });


           

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Todo}/{action=Index}/{id?}");
            });
        }
    }
}
