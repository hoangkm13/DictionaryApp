using System;
using System.ComponentModel.DataAnnotations;
using Dictionary.Service.Attributes;

namespace Dictionary.Service.Model;

[Table("concept")]
public class Concept 
{
    [Attributes.Key]
    public Guid concept_id { get; set; }

    public Guid dictionary_id { get; set; }
    
    [StringLength(255)]
    public string title { get; set; }
    
    [StringLength(500)]
    public string description { get; set; }
    
    public DateTime created_date { get; set; }
    
    public DateTime modified_date { get; set; }

}