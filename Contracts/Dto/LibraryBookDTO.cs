using Contracts.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Dto
{
    public class LibraryBookDTO
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "ISBN #")]
        public string? ISBN { get; set; }

        [Required]
        public string? Author { get; set; }

        [Required]
        [Display(Name = "DDC")]
        public string? DeweyIndex { get; set; }
        [Required]
        public string? Title { get; set; }

        [Required]
        public int Year { get; set; } // Just store as an int for BC

        [Required]
        public Status Status { get; set; }

        [Required, Display(Name = "Cost of Replacement")]
        public decimal Cost { get; set; }
        public string? ImageUrl { get; set; }
        public int NumberOfCopies { get; set; }
        public int? LocationId { get; set; }

    }
}
