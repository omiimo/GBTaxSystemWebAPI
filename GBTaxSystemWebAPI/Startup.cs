using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GBTaxSystemWebAPI.Filters;
using GBTaxSystemWebAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GBTaxSystemWebAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //Register app services
            services.AddTransient<IPurchaseService, PurchaseService>();

            services.AddControllers(options =>
            options.Filters.Add(new ApiExceptionFilter()));

            // Register the Swagger services
            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Purchase Data Calculator - Austria";
                    document.Info.Description = "Calculate Net, Gross, VAT amounts for your purchases in Austria. Valid VAT rates are: 10%, 13%, 20%";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "Omprakash",
                        Email = "omi@live.in"
                    };
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // Register the Swagger generator and the Swagger UI middlewares
            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
