using Contracts.Utils.Auditing;


namespace Contracts.Entities
{
    public class City : FullAuditedEntity
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public int StateId { get; set; }
        public virtual State? State { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public int? TenantId { get; set; }
        public int? MapId { get; set; }
    }
}
