using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using LifeManager.Data.Contexts;
using LifeManager.Data.Entities;
using LifeManager.Rest.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NServiceBus;

namespace LifeManager.Rest
{
    [ExcludeFromCodeCoverage]
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Isser"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))                        
                    };
                });            

            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlServer(Configuration["Connection"]);
            var db = new AuthContext(optionsBuilder.Options);
            var userStore = new UserStore<User>(db, null);
            var userManager = new UserManagerWrapper(userStore, null, new PasswordHasher<User>(), null, null, null, null, null, null);
            services.AddSingleton<IUserManagerWrapper>(userManager);

            var endpointConfiguration = new EndpointConfiguration("LifeManager.Rest");
            endpointConfiguration.UseTransport<RabbitMQTransport>().ConnectionString(Configuration["RabbitMqConnectionString"])
                .UseConventionalRoutingTopology();
            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            var guid = Guid.NewGuid();
            endpointConfiguration.MakeInstanceUniquelyAddressable(guid.ToString());
            endpointConfiguration.EnableInstallers();
            endpointConfiguration.EnableCallbacks();
            endpointConfiguration.LicensePath(Configuration["NServiceBusLicense"]);
            var endpoint = Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();
            services.AddSingleton(endpoint);

            db.Database.EnsureCreated();
            services.AddMvc(options =>
            {                
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }                     
            app.UseAuthentication();
            app.UseMvc();
        }        
    }
}
