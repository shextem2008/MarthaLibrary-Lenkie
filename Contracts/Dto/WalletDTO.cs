using Contracts.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contracts.Dto
{
    public class WalletDTO
    {
        public int Id { get; set; }
        public string? WalletNumber { get; set; }
        public WalletVendor WalletVendor { get; set; }
        public string? WalletVirAcctID { get; set; }
        public string? AccountNo { get; set; }
        public decimal Balance { get; set; }
        public decimal OldBalance { get; set; }
        public WalletStatus Status { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? WalletLastUpdated { get; set; }
        public int? TenantId { get; set; }
        public int? CreatorUserId { get; set; }
        
    }
}
