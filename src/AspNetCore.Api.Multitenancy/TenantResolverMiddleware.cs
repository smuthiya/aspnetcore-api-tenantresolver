using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AspNetCore.Api.TenantResolver
{
    public class TenantResolverMiddleware<T> where T : Tenant
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TenantResolverMiddleware<T>> _logger;

        public TenantResolverMiddleware(ILogger<TenantResolverMiddleware<T>> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Items.ContainsKey(Constants.TenantKey))
            {
                var tenantService = context.RequestServices.GetService(typeof(TenantRepository<T>)) as TenantRepository<T>;
                context.Items.Add(Constants.TenantKey, await tenantService.GetTenantAsync());
            }

            if (_next != null)
                await _next(context);
        }
    }
}
