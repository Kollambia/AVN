using System.ComponentModel.DataAnnotations;

namespace AVN.Model.Entities;

public class BaseEntity
{
    [Key]
    public int Id { get; set; }
}