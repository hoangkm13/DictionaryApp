using System;

namespace Dictionary.Service.DtoEdit.Dictionary;

public class CreateDictionary
{
    public string clone_dictionary_id { get; set; }
    public string dictionary_name { get; set; }
    public DateTime last_view_at { get; set; }
}