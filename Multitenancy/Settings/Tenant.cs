namespace Multitenancy.Settings
{
    public class Tenant
    {
        public string TId { get; set; }
        public string Name { get; set; }
        public string? ConnectionString { get; set; }
    }
}
