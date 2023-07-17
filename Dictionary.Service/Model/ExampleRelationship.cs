using System;
using Dictionary.Service.Attributes;

namespace Dictionary.Service.Model;

[Table("example_relationship")]
public class ExampleRelationship : BaseModel
{
    [Key]
    public Guid exampleRelationShipId { get; set; }
    public Guid dictionaryId { get; set; }
    public Guid contentId { get; set; }
    public Guid exampleId { get; set; }
    public Guid exampleLinkId { get; set; }
}