using Contracts.Utils.Auditing;

namespace Contracts.Entities
{
    public class Region : FullAuditedEntity
    {
        public string? Name { get; set; }
        public int? TenantId { get; set; }
    }
}
