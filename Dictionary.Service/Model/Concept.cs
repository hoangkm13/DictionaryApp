using System;
using System.ComponentModel.DataAnnotations;
using Dictionary.Service.Attributes;

namespace Dictionary.Service.Model;

[Table("concept")]
public class Concept : BaseModel
{
    [Attributes.Key]
    public Guid conceptId { get; set; }

    public Guid dictionaryId { get; set; }
    
    [StringLength(255)]
    public string title { get; set; }
    [StringLength(500)]
    public string description { get; set; }
}