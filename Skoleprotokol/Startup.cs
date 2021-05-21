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
            services.AddScoped<IAttendanceKeyService<AttendanceKeyDto, string>, AttendanceKeyService>();
            services.AddScoped<ILessonService<Int32, Int32>, LessonService>();

            // Auto mapper configuration
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapping()); // Our AutoMapping class in ./Config/
            });
            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddCors();

            services.AddControllers();
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
        }
    }
}
