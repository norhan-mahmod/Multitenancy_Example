namespace Multitenancy.Contracts
{
    public interface IMustHaveTenant
    {
        public string TenantId { get; set; }    
    }
}
