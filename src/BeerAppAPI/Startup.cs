using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using BeerAppAPI.Models;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.SwaggerUi;
using Swashbuckle.SwaggerGen;
using Swashbuckle.SwaggerGen.Generator;
using Swashbuckle.Swagger.Model;

namespace BeerAppAPI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets();
            }

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BeerDBContext>(options => options.UseSqlServer(Configuration["Data:DefaultConnection:DefaultConnectionString"]));

            services.AddSwaggerGen(c =>
            {
                c.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "BeerAPI",
                    Description = "An API to store information about beers and breweries native to Minnesota",
                    TermsOfService = "",
                    Contact = new Contact
                    {
                        Name = "Phil Jirsa",
                        Url = "http://twitter.com/pjirsa",
                        Email = "phil@36mph.com"
                    },
                    License = new License
                    {
                        Name = "MIT",
                        Url = "http://choosealicense.com/licenses/mit/"
                    }
                });

                c.AddSecurityDefinition("bearer", new ApiKeyScheme
                {
                    Type = "apiKey",
                    Name = "Authorization",
                    In = "header",
                    Description = "Azure AD JWT Token"
                });

                //c.OperationFilter<AssignSecurityRequirements>();

            });

            // Add framework services.

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            services.AddAuthentication();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseSwagger();
            app.UseSwaggerUi();

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                Authority = String.Format(Configuration["AzureAd:AadInstance"], Configuration["AzureAD:Tenant"]),
                Audience = Configuration["AzureAd:Audience"],
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
