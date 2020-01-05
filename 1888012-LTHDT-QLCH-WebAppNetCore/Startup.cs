using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _1888012_LTHDT_QLCH_WebAppNetCore.Models;
using _1888012_LTHDT_QLCH_WebAppNetCore.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace _1888012_LTHDT_QLCH_WebAppNetCore
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
            //Services to connect database
            services.AddDbContextPool<AppDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("UserDBConnection")));

            //Add built-in service Indentity for Authentication & Authorization
            services.AddIdentity<IdentityUser, IdentityRole>(
                    //customize property of Identity options
                    options => { 
                        options.Password.RequireNonAlphanumeric = false;
                        options.User.AllowedUserNameCharacters += " ";
                    }

                )
                .AddEntityFrameworkStores<AppDbContext>();

            services.AddControllersWithViews(options => {
                //Make a global authentication policy for our app
                var policy = new AuthorizationPolicyBuilder()
                                 .RequireAuthenticatedUser()
                                 .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });

            //Change AccessDenied Route
            services.ConfigureApplicationCookie(options => {
                options.AccessDeniedPath = new PathString("/Administration/AccessDenied");
            });

            //Add claim-based authorization
            //Remember that role is a claimType (a claim is a key - values pair)
            services.AddAuthorization(options =>
            {
                //To satisfy the policy, user must have all Claims prescribed
                //RequireClaim, RequireRole, RequireAssertion are all built-in
                options.AddPolicy("DeleteRolePolicy", 
                    policy => policy.RequireClaim("Delete Role")
                                    .RequireRole("Admin"));

                //Add policy with customized requirements -> Remember to register it as below
                //Handlers in one Requirement work as OR relation
                options.AddPolicy("EditRolePolicy",
                    policy => policy.AddRequirements( new ManageAdminRoleAndClaimRequirement()));
                
                //Use this option to customize - the whole requirement will fail if one handler fails
                //options.InvokeHandlersAfterFailure = false; //default is true

            });


            //Add DI service here 
            //AddScoped to request for a new service everytime it is called
            services.AddScoped<IProductRepository,MockProductRepository>();

            //Register for our customized requirements to be called
            //services.AddTransient<IHttpContextAccessor, HttpContextAccessor>(); - automatically inject???
            services.AddSingleton<IAuthorizationHandler, CanEditOnlyOtherAdminRoleAndClaimHandler>();
            services.AddSingleton<IAuthorizationHandler, SuperAdminHandler>();

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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //Routing pattern here
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
