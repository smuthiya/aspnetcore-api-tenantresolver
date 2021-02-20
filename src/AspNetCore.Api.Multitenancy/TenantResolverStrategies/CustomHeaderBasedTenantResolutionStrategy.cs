using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace AspNetCore.Api.TenantResolver
{
    public class CustomHeaderBasedTenantResolutionStrategy : ITenantResolutionStrategy
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string CustomHeaderKey = "x-tenant";

        public CustomHeaderBasedTenantResolutionStrategy(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<string> GetTenantIdentifierAsync()
        {
            StringValues values;
            if (_httpContextAccessor.HttpContext.Request.Headers.TryGetValue(CustomHeaderKey, out values))
            {
                return Task.FromResult(values.FirstOrDefault());
            }
            return Task.FromResult(string.Empty);
        }
    }
}