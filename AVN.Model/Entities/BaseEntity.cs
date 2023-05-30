using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace AVN.Model.Entities;

public class BaseEntity
{
    public int Id { get; set; }
}