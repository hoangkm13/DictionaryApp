using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dictionary.Service.Model;

[Table("sys_concept_link")]
public class SysConceptLink
{
    [Key]
    public Guid sysConceptLinkId { get; set; }
    public string conceptLinkName { get; set; }
    public int conceptLinkType { get; set; }
    public int sortOrder { get; set; }
}