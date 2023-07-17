using System;

namespace Dictionary.Service.DtoEdit.Dictionary;

public class UpdateDictionary
{
    public Guid dictionary_id { get; set; }
    public string dictionary_name { get; set; }
}