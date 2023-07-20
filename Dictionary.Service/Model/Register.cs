using System;
using Dictionary.Service.Attributes;

namespace Dictionary.Service.Model;

[Table("register")]
public class Register : BaseModel
{
    [Key]
    public Guid register_id { get; set; }
    public Guid sys_register_id { get; set; }
    public Guid user_id { get; set; }
    public string register_name { get; set; }
    public int register_type { get; set; }
    public int sort_order { get; set; }
}