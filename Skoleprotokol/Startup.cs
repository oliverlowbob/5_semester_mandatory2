using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Skoleprotokol.Models;
using Skoleprotokol.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Skoleprotokol.Services;
using Skoleprotokol.Dtos;
using Skoleprotokol.Config;

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
            // Database context configuration
            var mySqlConnectionString = Configuration.GetConnectionString("MySqlConnection");

            services.AddDbContext<SchoolProtocolContext>(options => options.UseLazyLoadingProxies()
                .UseMySql(mySqlConnectionString, ServerVersion.AutoDetect(mySqlConnectionString)));

            // User login authentication service configuration
            services.AddScoped<IAuthenticationService<UserAuthenticationDto>, AuthenticationService>();

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
