using EFCoreMultiSchema;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppTest.Database;

namespace WebAppTest
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
            services.AddHttpContextAccessor();
            services.AddTransient<AppDbContextFactory>();
            services.AddScoped<AppDbContextManager>();
            services.AddScoped<SchemaSelector>();
            services.AddTransient<AppDbContextOptionsBuilder>();
            services.AddDbContext<AppDbContext>((serviceProvider, optionsBuilder) =>
            {
                var schema = serviceProvider.GetRequiredService<SchemaSelector>().GetSchema();
                if (schema == null)
                    throw new ArgumentException("Não foi possível definir o esquema");

                serviceProvider.GetRequiredService<AppDbContextOptionsBuilder>().Configure(optionsBuilder, schema);
            });
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
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            using var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<AppDbContextFactory>().Create(AppDbContext.DefaultSchema);
            if (!dbContext.Database.DatabaseExists().GetAwaiter().GetResult())
                dbContext.Database.CreateDatabase().GetAwaiter().GetResult();
        }
    }
}
