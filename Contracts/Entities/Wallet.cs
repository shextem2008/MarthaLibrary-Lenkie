using Contracts.Entities.Enums;
using Contracts.Utils.Auditing;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Entities
{
    public class Wallet : FullAuditedEntity
    {
        [Display(Name = "Overdue Fees")]
        public decimal Fees { get; set; }
        [Required]
        public string? WalletNumber { get; set; }  
        public WalletVendor WalletVendor { get; set;}
        public string? WalletVirAcctID { get; set; }
        public string? AccountNo { get; set; }
        public decimal Balance { get; set; }
        public decimal OldBalance { get; set; }
        public WalletStatus Status { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? WalletLastUpdated { get; set; }
        public int? TenantId { get; set; }
    }
}
