using Contracts.Entities;
using Contracts.Entities.Enums;
using StakeHoldersWebApi.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Dto
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string? PasswordHash { get; set; }
        public string? PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string? MiddleName { get; set; }
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
        public string? UserPhotoId { get; set; }
        public int? WalletId { get; set; }
        public int? IdentificationId { get; set; }
        public int? TenantId { get; set; }
        public int? LocationId { get; set; }
        //public IList<string>? UserRoles { get; set; } 
        public IList<RoleDto>? UserRoles { get; set; }
        public IList<Claim>? RoleClaims { get; set; }
    }
}
