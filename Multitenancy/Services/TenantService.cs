
using Microsoft.Extensions.Options;

namespace Multitenancy.Services
{
    public class TenantService : ITenantService
    {
        private readonly TenantSettings _tenantSettings;
        private Tenant? _currentTenant;
        private HttpContext? _httpContext;
        public TenantService(IHttpContextAccessor httpContextAccessor , IOptions<TenantSettings> tenantSettings)
        {
            _httpContext = httpContextAccessor.HttpContext;
            _tenantSettings = tenantSettings.Value;
            if(_httpContext is not null)
            {
                if(_httpContext.Request.Headers.TryGetValue("tenant" , out var tenantId))
                {
                    SetCurrentTenant(tenantId);
                }
                else
                {
                    throw new Exception("No Tenant provided !");
                }
            }
        }
        public string? GetConnectionString()
        {
            var currentConnectionString = _currentTenant is null ? _tenantSettings.Defaults.ConnectionString : _currentTenant.ConnectionString;
            return currentConnectionString;
        }

        public Tenant? GetCurrentTenant()
            => _currentTenant;

        public string? GetDatabaseProvider()
            => _tenantSettings.Defaults.DBProvider;
        private void SetCurrentTenant(string tenantId)
        {
            _currentTenant = _tenantSettings.Tenants.FirstOrDefault(t => t.TId == tenantId);
            if (_currentTenant is null)
                throw new Exception(" Invalid Tenant Id");
            if (string.IsNullOrEmpty(_currentTenant.ConnectionString))
                _currentTenant.ConnectionString = _tenantSettings.Defaults.ConnectionString;
        }
    }
}
