using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Api.TenantResolver;

namespace TestAPIs
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

            var tenants = new List<Tenant> { new Tenant { Identifier = "localhost", Id = Guid.NewGuid().ToString() } };
            services.AddTenantResolver<Tenant>()
                .AddInMemoryTenantStore(tenants)
            .AddHostBasedTenantResolutionStrategy();
            //.AddCustomHeaderBasedTenantResolutionStrategy();
            //.AddRequestPathBasedTenantResolutionStrategy();
            //.AddResolutionStrategy<CustomHeaderBasedTenantResolutionStrategy>();
            //.AddResolutionStrategy<HostBasedTenantResolutionStrategy>();

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

            app.UseAuthorization();

            app.UseTenantResolver();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
