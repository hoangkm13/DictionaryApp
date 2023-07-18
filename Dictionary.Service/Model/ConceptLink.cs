using System;
using Dictionary.Service.Attributes;

namespace Dictionary.Service.Model;

[Table("concept_link")]
public class ConceptLink : BaseModel
{
    [Key]
    public Guid conceptLinkId { get; set; }

    public Guid sysConceptLinkId { get; set; }

    public string conceptLinkName { get; set; }

    public string conceptLinkType { get; set; }
    
    public int sortOrder { get; set; }
}