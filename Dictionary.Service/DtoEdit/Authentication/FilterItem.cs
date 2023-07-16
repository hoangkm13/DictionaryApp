using System.Collections.Generic;

namespace Dictionary.Service.DtoEdit.Authentication;

public class FilterItem
{
    public string Field { get; set; }
    public string Operator { get; set; }
    public object Value { get; set; }
    public List<FilterItem> Ors { get; set; }
    public string Alias { get; set; }
}