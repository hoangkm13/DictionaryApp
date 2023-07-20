using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dictionary.Service.Model;

[Table("dialect")]
public class Dialect : BaseModel
{
    [Key]
    public Guid dialect_d { get; set; }
    public Guid sys_dialect_id { get; set; }
    public Guid user_id { get; set; }
    public string dialect_name { get; set; }
    public int dialect_type { get; set; }
    public int sort_order { get; set; }
}