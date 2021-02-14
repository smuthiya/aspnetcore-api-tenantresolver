using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.Api.Multitenancy
{
    public static class ServiceCollectionExtensions
    {
        public static TenantResolutionBuilder<T> AddTenantResolver<T>(this IServiceCollection services) where T : Tenant
        {
            return new TenantResolutionBuilder<T>(services);
        }


        public static TenantResolutionBuilder<Tenant> AddTenantResolver(this IServiceCollection services)
        { 
            return new TenantResolutionBuilder<Tenant>(services); 
        }
    }
}