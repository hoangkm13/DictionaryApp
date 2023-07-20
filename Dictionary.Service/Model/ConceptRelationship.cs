using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dictionary.Service.Model;

[Table("concept_relationship")]
public class ConceptRelationship : BaseModel
{
    [Key]
    public Guid concept_relation_ship_id { get; set; }
    public Guid dictionary_id { get; set; }
    public Guid concept_id { get; set; }
    public Guid parent_id { get; set; }
    public Guid concept_link_id { get; set; }
}
