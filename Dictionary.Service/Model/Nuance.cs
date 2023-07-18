using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dictionary.Service.Model;

[Table("nuance")]
public class Nuance : BaseModel
{
    [Key]
    public Guid nuanceId { get; set; }
    public Guid sysNuanceId { get; set; }
    public Guid userId { get; set; }
    public string nuanceName { get; set; }
    public int nuanceType { get; set; }
    public int sortOrder { get; set; }
}