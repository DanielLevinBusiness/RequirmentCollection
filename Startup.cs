using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using OpsRequirmentCollectionWeb.Controllers;
using OpsRequirmentCollectionWeb.Models;

namespace OpsRequirmentCollectionWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }
        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
            {
            }

            public DbSet<EngBoards> EngBoards { get; set; }

            public DbSet<submitStatus> submitStatus { get; set; }

            public DbSet<BudgetItemENG> BudgetItemsENG { get; set; }

            public DbSet<BudgetItemENGApp> BudgetItemsENGApp { get; set; }

            public DbSet<BudgetItemPRO> BudgetItemsPRO { get; set; }

            public DbSet<BudgetItemPROApp> BudgetItemsPROApp { get; set; }

            public DbSet<BudgetItemSI> BudgetItemsSI { get; set; }

            public DbSet<BudgetItemSIApp> BudgetItemsSIApp { get; set; }

            public DbSet<Requestor> Requestors { get; set; }

            public DbSet<SiPackage> SiPackage { get; set; }

            public DbSet<ProductBoards> ProductBoards { get; set; }

            public DbSet<GroupAndTeam> GroupAndTeams { get; set; }
        }

        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
            Configuration.GetConnectionString("Mink")
                ));

            services.AddAuthentication(IISServerDefaults.AuthenticationScheme);

            services.AddControllersWithViews();
            
            services.AddHttpContextAccessor();

        }

        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AppDomain.CurrentDomain.SetData("ContentRootPath", env.ContentRootPath);
            AppDomain.CurrentDomain.SetData("WebRootPath", env.WebRootPath);

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
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

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