using System;
using Dictionary.Service.Attributes;

namespace Dictionary.Service.Model
{
    [Table("dictionary")]
    public class DictionaryEntity
    {
        [Key] public Guid dictionary_id { get; set; }

        public Guid user_id { get; set; }
        public string dictionary_name { get; set; }
        public DateTime last_view_at { get; set; }
        public DateTime created_at { get; set; }
        public DateTime modified_at { get; set; }
    }
}