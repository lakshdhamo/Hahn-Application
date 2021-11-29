using Hahn.ApplicatonProcess.July2021.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Hahn.ApplicatonProcess.July2021.Domain.Interfaces;
using Hahn.ApplicatonProcess.July2021.Data.Repositories;
using Hahn.ApplicatonProcess.July2021.Data.UnitOfWorks;
using Hahn.ApplicatonProcess.July2021.Domain.Interfaces.ServiceInterface;
using Hahn.ApplicatonProcess.July2021.Domain.ServiceManager;
using FluentValidation.AspNetCore;
using Serilog;
using System.Reflection;
using System.IO;
using Hahn.ApplicatonProcess.July2021.Web.swaggerExample;
using Swashbuckle.AspNetCore.Filters;

namespace Hahn.ApplicatonProcess.July2021.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            Log.Logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(configuration)
                        .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers().AddFluentValidation(s =>
            {
                s.RegisterValidatorsFromAssemblyContaining<Startup>();
            });

            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Hahn.ApplicatonProcess.July2021.Web",
                    Version = "v1",
                    Description = "The Web API endpoint to manage Hahn's User profile and Asset tracking."
                    
                });
                c.ExampleFilters();
            });
            services.AddSwaggerExamplesFromAssemblyOf<UserVmExample>();

            #region "Entity Framework Inmemory Database"
            services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase(databaseName: "UserProfile"));
            #endregion

            #region Repositories
            //services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //services.AddTransient<IUserRepository, UserRepository>();
            //services.AddTransient<IAssetRepository, AssetRepository>();
            #endregion

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<IAssetManager, AssetManager>();

            //Enabling CORS
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hahn.ApplicatonProcess.July2021.Web v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseAuthorization();

            // This will make the HTTP requests log as rich logs instead of plain text.
            app.UseSerilogRequestLogging();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
