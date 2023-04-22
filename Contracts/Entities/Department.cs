using Contracts.Entities.Enums;
using Contracts.Utils.Auditing;


namespace Contracts.Entities
{
    public class Department : FullAuditedEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int TenantId { get; set; }
    }
}
