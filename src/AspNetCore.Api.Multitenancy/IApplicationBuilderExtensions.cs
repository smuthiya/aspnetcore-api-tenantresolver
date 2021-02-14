using Microsoft.AspNetCore.Builder;

namespace AspNetCore.Api.Multitenancy
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseTenantResolver<T>(this IApplicationBuilder builder) where T : Tenant
        {
            return builder.UseMiddleware<TenantResolverMiddleware<T>>();
        }

        public static IApplicationBuilder UseTenantResolver(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TenantResolverMiddleware<Tenant>>();
        }
    }
}