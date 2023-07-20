using System;
using System.ComponentModel.DataAnnotations;
using Dictionary.Service.Attributes;

namespace Dictionary.Service.Model;

[Table("audit_log")]
public class AuditLog : BaseModel
{
    [Attributes.Key]
    public Guid audit_log_id { get; set; }
    public Guid user_id { get; set; }
    public string screen_info { get; set; }
    public int action_type { get; set; }
    public string reference { get; set; }
    [StringLength(500)]
    public string description { get; set; }
    public string user_agent { get; set; }
}