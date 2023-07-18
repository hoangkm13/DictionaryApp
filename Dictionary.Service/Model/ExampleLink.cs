using System;
using Dictionary.Service.Attributes;

namespace Dictionary.Service.Model;

[Table("example_link")]
public class ExampleLink : BaseModel
{
    [Key]
    public Guid exampleLinkId { get; set; }
    public Guid sysExampleLinkId { get; set; }
    public Guid userId { get; set; }
    public string exampleLinkName { get; set; }
    public int exampleLinkType { get; set; }
    public int sortOrder { get; set; }
}