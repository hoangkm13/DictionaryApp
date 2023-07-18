using System;
using Dictionary.Service.Attributes;

namespace Dictionary.Service.Model;

[Table("dictionary")]
public class Dictionary : BaseModel
{
    [Key]
    public Guid dictionaryId { get; set; }
    public Guid userId { get; set; }
    public string dictionaryName { get; set; }
    public DateTime lastViewAt { get; set; }
}