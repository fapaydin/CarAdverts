using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CarAdverts.Core.Entities;
using CarAdverts.Core.Interfaces;
using CarAdverts.Core.Services;
using CarAdverts.Infrastructure.Data;
using CarAdverts.Infrastructure.Logging;
using CarAdverts.Infrastructure.Repositories;
using CarAdverts.WebAPI.Filters;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Converters;
using Swashbuckle.AspNetCore.Swagger;

namespace CarAdverts.WebAPI
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
            //Add DbContext using SQL Server Provider
            services.AddDbContext<AdvertContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AdvertsDatabase")));

            services.AddScoped<IAdvertService, AdvertService>();
            services.AddScoped<IAdvertRepository, AdvertRepository>();
            services.AddScoped<IAppLogger<IAdvertService>, LoggerAdapter<IAdvertService>>();
            //Add Fluent Validation
            services.AddMvc(options =>
                    options.Filters.Add(typeof(CustomExceptionFilterAttribute)))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AdvertValidation>());


            services.AddSwaggerGen(c=> {
                c.SwaggerDoc("CarAdverts", new Info
                {
                    Title = "CarAdverts WebAPI",
                    Version = "1.0.0",
                    Description = "CarAdverts WebAPI v1.0.0",
                    Contact = new Contact()
                    {
                        Name = "CarAdverts Implementation Fevzi APAYDIN",
                        Url = "https://linkedin.com/in/fapaydin",
                        Email = "fapaydin@outlook.com"
                    }
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.AddFluentValidationRules();
            });

           
            //Add CORS Support
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin",
                    builder => builder
                        .AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
                       
                );
            });

            // Customise default API behavour
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = false;
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }

            

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/CarAdverts/swagger.json", "CarAdvert API V1");
            });


            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
