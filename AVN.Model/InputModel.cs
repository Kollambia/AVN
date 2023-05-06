using System.ComponentModel.DataAnnotations;

namespace AVN.Model
{
    public class InputModel
    {
        [Required]
        [Display(Name = "Role")]
        public string Role { get; set; }
    }
}
