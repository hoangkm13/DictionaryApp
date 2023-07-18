using System;
using Dictionary.Service.Attributes;

namespace Dictionary.Service.Model;

[Table("register")]
public class Register : BaseModel
{
    [Key]
    public Guid registerId { get; set; }
    public Guid sysRegisterId { get; set; }
    public Guid userId { get; set; }
    public string registerName { get; set; }
    public int registerType { get; set; }
    public int sortOrder { get; set; }
}