using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dictionary.Service.Model;

[Table("concept_relationship")]
public class ConceptRelationship : BaseModel
{
    [Key]
    public Guid conceptRelationShipId { get; set; }
    public Guid dictionaryId { get; set; }
    public Guid conceptId { get; set; }
    public Guid parentId { get; set; }
    public Guid coneptLinkId { get; set; }
}