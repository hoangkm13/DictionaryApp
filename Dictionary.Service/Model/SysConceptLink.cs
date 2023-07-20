using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dictionary.Service.Model;

[Table("sys_concept_link")]
public class SysConceptLink
{
    [Key]
    public Guid sys_concept_link_id { get; set; }
    public string concept_link_name { get; set; }
    public int concept_link_type { get; set; }
    public int sort_order { get; set; }
}