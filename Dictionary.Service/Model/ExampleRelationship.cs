using System;
using Dictionary.Service.Attributes;

namespace Dictionary.Service.Model;

[Table("example_relationship")]
public class ExampleRelationship : BaseModel
{
    [Key]
    public Guid example_relation_ship_id { get; set; }
    public Guid dictionary_id { get; set; }
    public Guid content_id { get; set; }
    public Guid example_id { get; set; }
    public Guid example_link_id { get; set; }
}