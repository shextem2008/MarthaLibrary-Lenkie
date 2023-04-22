using Contracts.Collections.Entities;


namespace Contracts.Entities
{
    public class State : Entity
    {
        public string? Name { get; set; }
        public int RegionId { get; set; }
        public Region? Region { get; set; }
        public int? TenantId { get; set; }
    }
}
