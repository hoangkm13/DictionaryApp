using System;
using Dictionary.Service.Attributes;

namespace Dictionary.Service.Model;

[Table("sys_dialect")]
public class SysDialect
{
    public Guid sys_dialect_id { get; set; }
    public string dialect_name { get; set; }
}