
using Contracts.Entities.Enums;
using Contracts.Utils.Auditing;
using StakeHoldersWebApi.Models.IdentityModels;

namespace Contracts.Entities
{
    public class Vendor : FullAuditedEntity
    {
        public int? UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }
        public string? VendorCode { get; set; }
        public string? VendorAddress { get; set; }
        public int? Location { get; set; }
        public int? City { get; set; }
        public int? State { get; set; }
        public int? Country { get; set; }
        public string? Fax { get; set; }
        public string? WebPage { get; set; }
        public string? Salutation { get; set; }
        public string? LGA { get; set; }
        public string? Landmark { get; set; }
        public DeviceType DeviceType { get; set; }
        public bool IsActive { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool OtpIsUsed { get; set; }


      


    }
}
