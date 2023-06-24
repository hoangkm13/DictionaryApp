using System.Collections.Generic;

namespace Dictionary.Service.DtoEdit.Authentication;

public class FilterTable
{
    public List<string> fields { get; set; }

    public List<string> sortBy { get; set; }
    public List<string> sortType { get; set; }
    public string filter { get; set; }

    public int page { get; set; }
    public int size { get; set; }
}