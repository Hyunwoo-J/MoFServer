using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureADB2C.UI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using MoFModel.Contexts;
using MoFModel.Models;

namespace MoFApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            Configuration = configuration;
            HostEnv = env;
        }

        public IConfiguration Configuration { get; }
        public IHostEnvironment HostEnv { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            //services.AddAuthentication(AzureADB2CDefaults.BearerAuthenticationScheme)
            //    .AddAzureADB2CBearer(options => Configuration.Bind("AzureAdB2C", options));

            services.AddControllers();

            services.AddAuthentication().AddFacebook(options =>
            {
                options.AppId = "203668571847239";
                options.AppSecret = "819d405904ca3aa21da303c003330ce5";
                options.AccessDeniedPath = "/Identity/Account/Login";
            });

            services.AddAuthentication().AddNaver(options =>
            {
                options.ClientId = "w9eehqWBlY_oheYT0Umv";
                options.ClientSecret = "2EyfP25Rdb";
                options.AccessDeniedPath = "/Identity/Account/Login";
            });

            services.AddAuthentication().AddKakaoTalk(options =>
            {
                options.ClientId = "f1ee30bf423b1e229b62ee5120d51d44";
                options.ClientSecret = "f1ee30bf423b1e229b62ee5120d51d44";
                options.AccessDeniedPath = "/Identity/Account/Login";
            });

            services.AddAuthentication().AddApple(options =>
            {
                options.ClientId = "com.hyunwoojang.mof.signin";
                options.KeyId = "4PV2Z3KAHP";
                options.TeamId = "694FXKZ628";

                options.UsePrivateKey((keyId) => HostEnv.ContentRootFileProvider.GetFileInfo($"AuthKey_{keyId}.p8"));
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
