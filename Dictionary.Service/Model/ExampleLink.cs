using System;
using Dictionary.Service.Attributes;

namespace Dictionary.Service.Model;

[Table("example_link")]
public class ExampleLink : BaseModel
{
    [Key]
    public Guid example_link_id { get; set; }
    public Guid sys_example_link_id { get; set; }
    public Guid user_id { get; set; }
    public string example_link_name { get; set; }
    public int example_link_type { get; set; }
    public int sort_order { get; set; }
}