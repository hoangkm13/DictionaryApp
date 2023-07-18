using System;
using System.ComponentModel.DataAnnotations;
using Dictionary.Service.Attributes;

namespace Dictionary.Service.Model;

[Table("audit_log")]
public class AuditLog : BaseModel
{
    [Attributes.Key]
    public Guid auditLogId { get; set; }
    public Guid userId { get; set; }
    public string screenInfo { get; set; }
    public int actionType { get; set; }
    public string reference { get; set; }
    [StringLength(500)]
    public string description { get; set; }
    public string userAgent { get; set; }
}