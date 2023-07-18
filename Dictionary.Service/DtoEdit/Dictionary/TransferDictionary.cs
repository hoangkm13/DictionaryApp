using System;

namespace Dictionary.Service.DtoEdit.Dictionary;

public class TransferDictionary
{
    public Guid source_dictionary_id { get; set; }
    public Guid dest_dictionary_id { get; set; }
    public Boolean is_delete_dest_data { get; set; }
}