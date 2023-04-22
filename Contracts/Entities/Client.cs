
using Contracts.Entities.Enums;
using Contracts.Utils.Auditing;
using StakeHoldersWebApi.Models.IdentityModels;

namespace Contracts.Entities
{
    public class Client : FullAuditedEntity
    {
        public int? UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }
        public string? ClientCode { get; set; }
        public string? ClientAddress { get; set; }
        public int? Location { get; set; }
        public int? City { get; set; }
        public int? State { get; set; }
        public int? Country { get; set; }
        public string? ClientFax { get; set; }
        public string? ClientWebPage { get; set; }
        public string? ClientSalutation { get; set; }
        public string? LGA { get; set; }
        public string? Landmark { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsActive { get; set; }
        public int? TenantId { get; set; }
      
        public string? Otp { get; set; }
        public bool OtpIsUsed { get; set; }
        public DateTime? OTPLastUsedDate { get; set; }
        public int? OtpNoOfTimeUsed { get; set; }
        public DeviceType DeviceType { get; set; }

    }
}
