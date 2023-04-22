using Contracts.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Dto
{
    public class BookCheckDTO
    {
        public int Id { get; set; }
        public int? LibraryBookId { get; set; }
        public int? CardId { get; set; }
        public Status Status { get; set; }
        public DateTime? Since { get; set; }
        public DateTime? Until { get; set; }
        public DateTime? Checkout { get; set; }
        public DateTime? CheckIn { get; set; }
    }
}
