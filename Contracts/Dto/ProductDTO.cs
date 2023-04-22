using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contracts.Dto
{
    public class ProductDTO
    {
        public int ProductId { get; set; } 
        public string? ProductName { get; set; }  
        public string? ProductCode { get; set; }
        public decimal ProductPrice { get; set; }
    }
}
