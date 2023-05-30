using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace AVN.Model.Entities;

public class BaseEntity<T, TId>
{
    [Key]
    public TId Id { get; set; }
}