using System.ComponentModel.DataAnnotations;
using AVN.Common.Enums;
using AVN.Model.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AVN.Models
{
    public class ScheduleVM : BasicVM<int>
    {
        [Required]
        public DaysOfTheWeek Days { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }

        [Required(ErrorMessage = "Выберите группу")]
        [Display(Name = "Группа")]
        public string GroupId { get; set; }

        public Group Group { get; set; }

        [Required(ErrorMessage = "Выберите предмет")]
        [Display(Name = "Группа")]
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        [Required(ErrorMessage = "Выберите преподавателя")]
        [Display(Name = "Группа")]
        public string EmployeeId { get; set; }

        public Employee Employee { get; set; }

        public IEnumerable<SelectListItem> SubjectSelectList { get; set; }
        public IEnumerable<SelectListItem> EmployeeSelectList { get; set; }
        public IEnumerable<SelectListItem> GroupSelectList { get; set; }

        public List<Schedule> Schedules { get; set; }
    }
}

