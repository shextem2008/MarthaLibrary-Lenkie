using Contracts.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Dto
{
    public class VendorDTO
    {
        public int Id { get; set; }
        public string? VendorCode { get; set; }
        public string? VendorAddress { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        //public string? FullName => FirstName + " " + LastName;
        public string? MiddleName { get; set; }
        public string? UserName { get; set; }       
        public Gender Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public string? VendorPhoto { get; set; }
        public string? NextOfKin { get; set; }
        public string? NextOfKinPhone { get; set; }
        public int? WalletId { get; set; }
        public int? Location { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int? City { get; set; }
        public int? State { get; set; }
        public int? IdentificationId { get; set; }
        public int? Country { get; set; }
        public bool IsActive { get; set; }
        public int? TenantId { get; set; }
        public int? UserId { get; set; }
        public DeviceType DeviceType { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string? RoleName { get; set; }
        public string? VendorFax { get; set; }
        public string? VendorWebPage { get; set; }
        public string? VendorSalutation { get; set; }

        public string? LGA { get; set; }
        public string? Landmark { get; set; }

        public string? Otp { get; set; }
        public bool OtpIsUsed { get; set; }
        public DateTime? OTPLastUsedDate { get; set; }
        public int? OtpNoOfTimeUsed { get; set; }







    }
}
