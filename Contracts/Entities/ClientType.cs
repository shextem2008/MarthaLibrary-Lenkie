
using Contracts.Collections.Entities;
using Contracts.Entities.Enums;
using Contracts.Utils.Auditing;
using StakeHoldersWebApi.Models.IdentityModels;

namespace Contracts.Entities
{
    public class ClientType : Entity
    {
        public int? TenantId { get; set; }
        public string? Name { get; set; }
        public string? ClientTypeDescription { get; set; }
        public string? SalesControlAccount { get; set; }
        public string? COSControlAccount { get; set; }
        public string? DebtorsControlAccount { get; set; }
        public int? CurrencyExchange { get; set; }
        public string? GainOrLossAccount { get; set; }
        public string? DiscountsAccount { get; set; }
        public string? DiscountRate { get; set; }
        public int? BillingType { get; set; }
    }
}
