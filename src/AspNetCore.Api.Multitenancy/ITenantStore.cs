using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.Api.TenantResolver
{
    public interface ITenantStore<T>
    {
        Task<T> GetTenantAsync(string identifier);
    }
}
