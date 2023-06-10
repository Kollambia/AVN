using System.ComponentModel.DataAnnotations;

namespace AVN.Model.Entities;

public class BaseEntity<T, TId>
{
    [Key]
    public TId Id { get; set; }
}