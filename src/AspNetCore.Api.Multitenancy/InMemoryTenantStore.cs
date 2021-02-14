using System.Threading.Tasks;
using System.Linq;
using System;

namespace AspNetCore.Api.Multitenancy
{
    public class InMemoryTenantStore : ITenantStore<Tenant>
    {
        private readonly Tenant[] _tenants;

        //public InMemoryTenantStore()
        //{
        //    _tenants = new[] { new Tenant { Identifier = "localhost", Id = Guid.NewGuid().ToString() } };
        //}

        public InMemoryTenantStore(Tenant[] tenants)
        {
            _tenants = tenants;
        }

        public Task<Tenant> GetTenantAsync(string identifier)
        {
            var tenant = _tenants.Where(t => t.Identifier == identifier).FirstOrDefault();
            return Task.FromResult(tenant);
        }
    }
}