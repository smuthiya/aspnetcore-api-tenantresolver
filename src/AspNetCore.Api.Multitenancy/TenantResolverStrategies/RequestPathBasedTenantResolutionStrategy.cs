using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace AspNetCore.Api.Multitenancy
{
    public class RequestPathBasedTenantResolutionStrategy : ITenantResolutionStrategy
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RequestPathBasedTenantResolutionStrategy(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<string> GetTenantIdentifierAsync()
        {
            //TODO:_httpContextAccessor.HttpContext.Request.Path.Value
            return Task.FromResult(string.Empty);
        }
    }
}