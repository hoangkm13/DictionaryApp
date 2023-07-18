using System;
using Dictionary.Service.Attributes;

namespace Dictionary.Service.Model;

[Table("sys_dialect")]
public class SysDialect
{
    public Guid sysDialectId { get; set; }
    public string dialectName { get; set; }
}