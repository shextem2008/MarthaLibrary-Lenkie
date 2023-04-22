using Contracts.Utils.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Entities
{
    public class Identification : FullAuditedEntity
    {
        public string? IdentificationCode { get; set; }
        public IdentificationType IdentificationType { get; set; }
        public DateTime IDIssueDate { get; set; }
        public DateTime IDExpireDate { get; set; }
        public int? IdentityPhotoID { get; set; }
        public int? TenantId { get; set; }


    }
}
