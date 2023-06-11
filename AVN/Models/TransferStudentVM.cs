using AVN.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using AVN.Model.Entities;

namespace AVN.Models
{
    public class TransferStudentVM : BasicVM<string>
    {
        public string? SName { get; set; }

        public string? Name { get; set; }

        public string? PName { get; set; }

        [DisplayName("ФИО Студента")]
        public string FullName => $"{SName} {Name} {PName}";

        [DisplayName("Зачётка")]
        public string? GradeBookNumber { get; set; }

        [DisplayName("Группа")]
        public int GroupId { get; set; }
        public Group? Group { get; set; }

        public bool Selected { get; set; }
        public bool Transfered { get; set; }
    }
}
