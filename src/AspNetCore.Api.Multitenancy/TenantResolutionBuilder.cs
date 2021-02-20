using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspNetCore.Api.TenantResolver
{
    public class TenantResolutionBuilder<T> where T : Tenant
    {
        private readonly IServiceCollection _services;

        public TenantResolutionBuilder(IServiceCollection services)
        {
            services.AddTransient<TenantRepository<T>>();
            _services = services;
        }

        public TenantResolutionBuilder<T> AddResolutionStrategy<V>(ServiceLifetime lifetime = ServiceLifetime.Transient) where V : class, ITenantResolutionStrategy
        {
            _services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            _services.Add(ServiceDescriptor.Describe(typeof(ITenantResolutionStrategy), typeof(V), lifetime));
            return this;
        }
        
        public TenantResolutionBuilder<T> AddTenantStore<V>(ServiceLifetime lifetime = ServiceLifetime.Transient) where V : class, ITenantStore<T>
        {
            _services.Add(ServiceDescriptor.Describe(typeof(ITenantStore<T>), typeof(V), lifetime));
            return this;
        }

        public TenantResolutionBuilder<T> AddInMemoryTenantStore(IEnumerable<Tenant> tenants)
        {
            _services.AddSingleton(tenants.ToArray());
            _services.AddTransient<ITenantStore<Tenant>, InMemoryTenantStore>();
            return this;
        }

        public TenantResolutionBuilder<T> AddHostBasedTenantResolutionStrategy()
        {
            _services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            _services.AddTransient<ITenantResolutionStrategy, HostBasedTenantResolutionStrategy>();
            return this;
        }

        public TenantResolutionBuilder<T> AddCustomHeaderBasedTenantResolutionStrategy()
        {
            _services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            _services.AddTransient<ITenantResolutionStrategy, CustomHeaderBasedTenantResolutionStrategy>();
            return this;
        }

        public TenantResolutionBuilder<T> AddRequestPathBasedTenantResolutionStrategy()
        {
            _services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            _services.AddTransient<ITenantResolutionStrategy, RequestPathBasedTenantResolutionStrategy>();
            return this;
        }
    }
}
