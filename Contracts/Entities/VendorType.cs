
using Contracts.Collections.Entities;
using Contracts.Entities.Enums;
using Contracts.Utils.Auditing;
using StakeHoldersWebApi.Models.IdentityModels;

namespace Contracts.Entities
{
    public class VendorType : Entity
    {
        public int? TenantId { get; set; }
        public string? Name { get; set; }
        public string? VendorTypeDescription { get; set; }
        public string? CreditorsControlAccount { get; set; }
        public string? CurrencyExchange { get; set; }
        public string? GainOrLossAccount { get; set; }
        public string? DiscountsAccount { get; set; }
        public string? DiscountRate { get; set; }
    }
}
