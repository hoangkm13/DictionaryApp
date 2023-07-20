using System;
using Dictionary.Service.Attributes;

namespace Dictionary.Service.Model;

[Table("example")]
public class Example : BaseModel
{
    [Key] 
    public Guid example_id { get; set; }
    public string dictionary_id { get; set; }
    public string detail { get; set; }
    public string detail_html { get; set; }
    public string note { get; set; }
    public string tone_id { get; set; }
    public string register_id { get; set; }
    public string dialect_id { get; set; }
    public string mode_id { get; set; }
    public string nuance_id { get; set; }
}