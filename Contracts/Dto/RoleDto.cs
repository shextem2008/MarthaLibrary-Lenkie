using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Dto
{
    public class RoleDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreationTime { get; set; }
        public bool IsDefaultRole { get; set; }
        public string? RolesDescription { get; set; }
        public int? TenantId { get; set; }
    }
}
