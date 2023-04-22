using Contracts.Entities;
using Contracts.Entities.Enums;
using Microsoft.AspNetCore.Identity;

namespace StakeHoldersWebApi.Models.IdentityModels
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public bool IsFirstTimeLogin { get; set; }
        public string? OptionalPhoneNumber { get; set; }
        public UserType UserType { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        public string? RefreshToken { get; set; }
        public string? Title { get; set; }
        public string? DeviceToken { get; set; }
        public string? Referrer { get; set; }
        public string? ReferralCode { get; set; }
        public string? NextOfKinName { get; set; }
        public string? NextOfKinPhone { get; set; }
        public DeviceType DeviceType { get; set; }    
        public Gender Gender { get; set; }
        public string? DateOfBirth { get; set; }
        public string? AccountConfirmationCode { get; set; }
        public string? OTP { get; set; }
        public string? ImageFileId { get; set; }
        //public virtual ImageFile? ImageFile { get; set; }
        public int? WalletId { get; set; }
        public virtual Wallet? Wallet { get; set; }
        public int? IdentificationId { get; set; }
        public virtual Identification? Identification { get; set; }
        public int? TenantId { get; set; }     
        public virtual Tenant? tenant { get; set; } 
        public int? LocationId { get; set; }
        public virtual Location? Location { get; set; }
        public ICollection<ApplicationUserRole> UserRoles { get; set; }

    }
}
