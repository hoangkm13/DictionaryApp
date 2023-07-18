using System;
using Dictionary.Service.Attributes;

namespace Dictionary.Service.Model;

[Table("example")]
public class Example : BaseModel
{
    [Key] 
    public Guid exampleId { get; set; }
    public string dictionaryId { get; set; }
    public string detail { get; set; }
    public string detailHtml { get; set; }
    public string note { get; set; }
    public string toneId { get; set; }
    public string registerId { get; set; }
    public string dialectId { get; set; }
    public string modeId { get; set; }
    public string nuanceId { get; set; }
}