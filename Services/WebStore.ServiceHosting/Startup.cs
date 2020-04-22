using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using WebStore.DAL.Context;
using WebStore.Data;
using WebStore.Domain.Entities.Identity;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Services.InCookies;
using WebStore.Infrastructure.Services.InMemory;
using WebStore.Infrastructure.Services.InSQL;
using WebStore.Logger;

namespace WebStore.ServiceHosting
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            #region Сервисы бизнес-логики
            services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();
            services.AddScoped<IProductData, SqlProductData>();
            services.AddScoped<IOrderService, SqlOrderService>();
            services.AddScoped<ICartService, CookiesCartService>();
            #endregion

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            #region Database
            services.AddDbContext<WebStoreDB>(opt =>
            opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<WebStoreDBInitializer>();
            #endregion

            #region Identity
            services.AddIdentity<User, Role>(opt =>
            {
                opt.Password.RequiredLength = 3;
                opt.Password.RequireDigit = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredUniqueChars = 3;

                opt.User.RequireUniqueEmail = false;

                opt.Lockout.AllowedForNewUsers = true;
                opt.Lockout.MaxFailedAccessAttempts = 10;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            })
                .AddEntityFrameworkStores<WebStoreDB>()
                .AddDefaultTokenProviders();
            #endregion

            services.AddSwaggerGen(opt => 
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "WebStore.API", Version = "v1" });

                opt.IncludeXmlComments("WebStore.ServiceHosting.xml");

                const string domain_doc_xml = "WebStore.Domain.xml";
                const string debug_puth = @"bin\Debug\netcoreapp3.1";
                if (File.Exists(domain_doc_xml))
                    opt.IncludeXmlComments(domain_doc_xml);
                else if (File.Exists(Path.Combine(debug_puth, domain_doc_xml)))
                    opt.IncludeXmlComments(Path.Combine(debug_puth, domain_doc_xml));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, WebStoreDBInitializer db, ILoggerFactory log)
        {
            log.AddLog4Net();

            db.Initialize();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(opt => {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "WebStore.API");
                opt.RoutePrefix = string.Empty;
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
