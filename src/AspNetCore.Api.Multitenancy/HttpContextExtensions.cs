using Microsoft.AspNetCore.Http;

namespace AspNetCore.Api.TenantResolver
{
    public static class HttpContextExtensions
    {
        public static T GetTenant<T>(this HttpContext context) where T : Tenant
        {
            if (!context.Items.ContainsKey(Constants.TenantKey))
                return null;
            return context.Items[Constants.TenantKey] as T;
        }

        public static Tenant GetTenant(this HttpContext context)
        {
            return context.GetTenant<Tenant>();
        }
    }
}