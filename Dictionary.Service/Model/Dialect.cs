using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dictionary.Service.Model;

[Table("dialect")]
public class Dialect : BaseModel
{
    [Key]
    public Guid dialectId { get; set; }
    public Guid sysDialectId { get; set; }
    public Guid userId { get; set; }
    public string dialectName { get; set; }
    public int dialectType { get; set; }
    public int sortOrder { get; set; }
}