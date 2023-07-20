using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dictionary.Service.Model;

[Table("nuance")]
public class Nuance : BaseModel
{
    [Key]
    public Guid nuance_id { get; set; }
    public Guid sys_nuance_id { get; set; }
    public Guid user_id { get; set; }
    public string nuance_name { get; set; }
    public int nuance_type { get; set; }
    public int sort_order { get; set; }
}