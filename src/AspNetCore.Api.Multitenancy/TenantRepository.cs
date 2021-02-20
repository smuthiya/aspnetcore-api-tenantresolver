using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AspNetCore.Api.TenantResolver
{
    public class TenantRepository<T> where T : Tenant
    {
        private readonly ILogger<TenantRepository<T>> _logger;
        private readonly ITenantResolutionStrategy _tenantResolutionStrategy;
        private readonly ITenantStore<T> _tenantStore;

        public TenantRepository(ILogger<TenantRepository<T>> logger, ITenantResolutionStrategy tenantResolutionStrategy, ITenantStore<T> tenantStore)
        {
            _logger = logger;
            _tenantResolutionStrategy = tenantResolutionStrategy;
            _tenantStore = tenantStore;
        }

        public async Task<T> GetTenantAsync()
        {
            var tenantIdentifier = await _tenantResolutionStrategy.GetTenantIdentifierAsync();
            return await _tenantStore.GetTenantAsync(tenantIdentifier);
        }
    }
}