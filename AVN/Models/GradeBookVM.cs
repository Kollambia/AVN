using System.ComponentModel.DataAnnotations;
using AVN.Common.Enums;
using AVN.Model.Entities;

namespace AVN.Models
{
    public class GradeBookVM : BasicVM<int>
    {
        [Required(ErrorMessage = "Выберите оценку")]
        [Display(Name = "Оценка")]
        public Grades Grade { get; set; }

        [Required(ErrorMessage = "Введите баллы")]
        [Display(Name = "Кол-во баллов")]
        public int Points { get; set; }

        [Required(ErrorMessage = "Введите дату сдачу")]
        [Display(Name = "Дата сдачи")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Выберите форму контроля")]
        [Display(Name = "Форма итогового контроля")]
        public FinalControlForm ControlForm { get; set; }

        [Required(ErrorMessage = "Введите кол-во кредитов")]
        [Display(Name = "Кол-во кредитов")]
        public int CreditsAmount { get; set; }

        [Required(ErrorMessage = "Введите кол-во часов по учебному плану")]
        [Display(Name = "Кол-во часов по учебному плану")]
        public int SyllabusHours { get; set; }

        [Required(ErrorMessage = "Выберите группу")]
        [Display(Name = "Группа")]
        public string GroupId { get; set; }
        public Group Group { get; set; }

        [Required(ErrorMessage = "Выберите предмет")]
        [Display(Name = "Предмет")]
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        [Required(ErrorMessage = "Выберите студента")]
        [Display(Name = "Студент")]
        public string StudentId { get; set; }
        public Student Student { get; set; }
    }
}
