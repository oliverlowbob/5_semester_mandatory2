using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Skoleprotokol.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Skoleprotokol.Services;
using Skoleprotokol.Dtos;
using Skoleprotokol.Config;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.IO;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Skoleprotokol
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
            var mySqlConnectionString = Configuration.GetConnectionString("MySqlConnection");

            services.AddDbContextFactory<SchoolProtocolContext>(options => 
                options.UseLazyLoadingProxies().UseMySql(mySqlConnectionString, ServerVersion.AutoDetect(mySqlConnectionString)).ConfigureWarnings(w => w.Ignore(CoreEventId.LazyLoadOnDisposedContextWarning)));

            services.AddScoped<IAuthenticationService<UserLoginDto>, AuthenticationService>();
            services.AddScoped<IUserService<UserDto, NewUserDto>, UserService>(); 
            services.AddScoped<IAttendanceKeyService<AttendanceKeyDto, string, int>, AttendanceKeyService>();
            services.AddScoped<ILessonService<string>, LessonService>();
            services.AddScoped<ICourseService<CourseDto>, CourseService>();

            // Auto mapper configuration
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapping()); // Our AutoMapping class in ./Config/
            });
            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddCors();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ToDo API",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Shayne Boyer",
                        Email = string.Empty,
                        Url = new Uri("https://twitter.com/spboyer"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials
            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API");
            });

        }
    }
}
