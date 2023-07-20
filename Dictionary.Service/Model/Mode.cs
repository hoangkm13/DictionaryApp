using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dictionary.Service.Model;

[Table("mode")]
public class Mode : BaseModel
{
    [Key]
    public Guid mode_id { get; set; }
    public Guid sys_mode_id{ get; set; }
    public Guid user_id { get; set; }
    public string mode_name { get; set; }
    public int mode_type { get; set; }
    public int sort_order { get; set; }
}