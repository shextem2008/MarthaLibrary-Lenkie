using Contracts.Entities.Enums;
using Contracts.Utils.Auditing;


namespace Contracts.Entities
{
    public class ImageFile : FullAuditedEntity
    {
        public string? FileName { get; set; }
        public string? Code { get; set; }
        public string? FileReference { get; set; }
        public UploadType UploadType { get; set; }
        public UploadFormat UploadFormat { get; set; }
        public string? UploadPath { get; set; }
        public string? Description { get; set; }
        public int UserId { get; set; }
        public int TenantId { get; set; }
    }
}
