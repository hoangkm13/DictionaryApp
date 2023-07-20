using System;
using Dictionary.Service.Attributes;

namespace Dictionary.Service.Model;

[Table("concept_link")]
public class ConceptLink : BaseModel
{
    [Key]
    public Guid concept_link_id { get; set; }

    public Guid sys_concept_link_id { get; set; }

    public string concept_link_name { get; set; }

    public string concept_link_type { get; set; }
    
    public int sort_order { get; set; }
}