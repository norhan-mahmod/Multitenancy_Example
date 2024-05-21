﻿namespace Multitenancy.Settings
{
    public class TenantSettings
    {
        public Configuration Defaults { get; set; }
        public List<Tenant> Tenants { get; set; } = new List<Tenant>();
    }
}
