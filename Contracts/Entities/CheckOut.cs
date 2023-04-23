
using Contracts.Entities.Enums;
using Contracts.Utils.Auditing;
using StakeHoldersWebApi.Models.IdentityModels;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Entities
{
    public class CheckOut : FullAuditedEntity
    {
        public int? LibraryBookId { get; set; }
        public virtual LibraryBook? LibraryBook { get; set; }
        public int? CardId { get; set; }
        public virtual Wallet? Card { get; set; }
        public Status Status { get; set; }
        public DateTime? ReserveUntil { get; set; }
        public DateTime? Since { get; set; }
        public DateTime? Until { get; set; }
        public DateTime? Checkout { get; set; }
        public DateTime? CheckIn { get; set; }
    }
}
