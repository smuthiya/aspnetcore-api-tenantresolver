using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.Api.Multitenancy
{
    public class HostBasedTenantResolutionStrategy : ITenantResolutionStrategy 
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HostBasedTenantResolutionStrategy(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> GetTenantIdentifierAsync()
        {
            return await Task.FromResult(_httpContextAccessor.HttpContext.Request.Host.Host);
        }
    }
}
