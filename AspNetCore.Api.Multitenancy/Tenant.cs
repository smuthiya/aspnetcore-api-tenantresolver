using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Api.Multitenancy
{
    public class Tenant
    {
        public string Id { get; set; }
        public string Identifier { get; set; }
    }
}
