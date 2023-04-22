using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Dto
{
    public class TenantSeedDTO
    {
        public int? DepartmentId { get; set; }
        public IList<string>? RoleNames { get; set; }
        public int? RegionvID { get; set; }
        public int? StatevID { get; set; }
        public int? cityvID { get; set; }
        public int? locationvID { get; set; }
        public int? IdentityvID { get; set; }
        public int? ImagevID { get; set; }

    }
}
