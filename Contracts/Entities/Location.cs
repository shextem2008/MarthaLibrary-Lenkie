using Contracts.Entities.Enums;
using Contracts.Utils.Auditing;


namespace Contracts.Entities
{
    public class Location : FullAuditedEntity
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Image { get; set; }
        public string? Address { get; set; }
        public string? ContactPerson { get; set; }
        public string? ContactPersonNo { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public DateTime TerminalStartDate { get; set; }
        public BranchType BranchType { get; set; }
        public int CityId { get; set; }
        public virtual City? City { get; set; }
        public string? TerminalCode { get; set; }
        public bool IsNew { get; set; }
        public bool IsCommision { get; set; }
        public decimal OnlineDiscount { get; set; }
        public bool IsOnlineDiscount { get; set; }
        public int? TenantId { get; set; }
    }
}
