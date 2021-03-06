using System;
using System.IO;
using System.Reflection;
using github_webscraping.Business;
using github_webscraping.Business.Implementations;
using github_webscraping.Repository;
using github_webscraping.Repository.Implementations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace github_webscraping
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
            services.AddControllers();

            //api versions
            services.AddApiVersioning(option => option.ReportApiVersions = true);
            //swagger configurations
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "WebScraping Github Api",
                    Description = "Api for get lines and bytes a public repository in github",
                    TermsOfService = new Uri("https://github.com/rafael-hs"),
                    Contact = new OpenApiContact
                    {
                        Name = "Rafael Hon�rio",
                        Email = "rafael.contatotrab@gmail.com",
                        Url = new Uri("https://rafael-hs.github.io/rafahs/#/")
                    }
            });
                //Locate the XML file being generated by ASP.NET...
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                //// and tell Swagger to use those XML comments.
                //c.IncludeXmlComments(xmlPath);
            });



            //Dependency Injection
            services.AddScoped<IGitHubRepoBusiness, GitHubRepoBusinessImpl>();
            services.AddScoped<IGitHubMappingRepository, GitHubMappingRepositoryImpl>();
        }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebScraping Github Api");
        });

        app.UseHttpsRedirection();

        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
}
