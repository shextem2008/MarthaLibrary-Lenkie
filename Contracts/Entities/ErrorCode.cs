using Contracts.Collections.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Entities
{
    public class ErrorCode : Entity
    {
        public string? Code { get; set; }
        public string? Message { get; set; }
        public string? Description { get; set; }
    }
}
