
using Contracts.Utils.Auditing;

namespace Contracts.Entities
{
    public class Tenant : FullAuditedEntity
    {
        //for personal detail
        public string? TenantNameA { get; set; }
        public string? TenantNameB { get; set; }
        public string? Tenantdesc { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? ContactPerson { get; set; }
        public string? ContactPhoneNo { get; set; }

        //public int? LocationId { get; set; }
        //public virtual Location? Location { get; set; }

        public string? Language { get; set; }
        public string? CompanySizeRange { get; set; }
        public string? PrimaryInterest { get; set; }
        public int? location { get; set; }
        public int? City { get; set; }
        public int? State { get; set; }
        public int? Country { get; set; }

        //For Company Settings
        public string? BranchedFrom { get; set; }
        public bool? IsParentCompany { get; set; }
        public DateTime? IsParentDate { get; set; }
        public string? HostingUrl { get; set; }
        public string? TenantUrl { get; set; }
        public bool IsActive { get; set; }


        //for Transport Settings 
        public int? BookingDaysRange { get; set; }
        public bool? IsTranspRecieved { get; set; }


        //for Payroll Settings
        public DateTime? PilotPayrollDate { get; set; }

        //for Email Settings
        public string? MJAPIKEYPRIVATE { get; set; }
        public string? MJAPIKEYPUBLIC { get; set; }
        public string? MJSenderEmail { get; set; }
        public string? MJSenderName { get; set; }

        //for Digital Ocean Setting
        public string? DOAccessKey { get; set; }
        public string? DOSecretKey { get; set; }
        public string? bucketName { get; set; }
        public string? DOHostUploadURL { get; set; }

        //for PayStack Setting
        public string? PYPublicKey { get; set; }
        public string? PYSecretKey { get; set; }

        //for Flutter Setting
        public string? FlPublicKey { get; set; }
        public string? FlSecretKey { get; set; }

        //for Sms Setting
        public bool? SmsEnableSSl { get; set; }
        public string? SmsPort { get; set; }
        public string? SmsServer { get; set; }
        public string? SmsPassword { get; set; }
        public string? SmsUserName { get; set; }
        public bool? SmsUseDefaultCred { get; set; }
        public string? SmsUrl { get; set; }

        //for file download settings
        public string? DocDownloadPath { get; set; }
        public string? ImageUrlPath { get; set; }

    }
}
