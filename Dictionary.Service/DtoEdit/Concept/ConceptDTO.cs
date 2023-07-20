using System;
using System.ComponentModel.DataAnnotations;

namespace Dictionary.Service.DtoEdit;

public class ConceptDTO
{
    public Guid dictionary_id { get; set; }
    
    [Required]
    public string title { get; set; }

    public string description { get; set; }
}