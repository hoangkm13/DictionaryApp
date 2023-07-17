using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dictionary.Service.Model;

[Table("mode")]
public class Mode : BaseModel
{
    [Key]
    public Guid modeId { get; set; }
    public Guid sysModeId { get; set; }
    public Guid userId { get; set; }
    public string modeName { get; set; }
    public int modeType { get; set; }
    public int sortOrder { get; set; }
}