using AVN.Common.Enums;
using AVN.Model.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AVN.Models
{
    public class ScheduleVM : BasicVM<int>
    {
        public DaysOfTheWeek Days { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string GroupId { get; set; }
        public Group Group { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public IEnumerable<SelectListItem> SubjectSelectList { get; set; }
        public IEnumerable<SelectListItem> EmployeeSelectList { get; set; }
        public IEnumerable<SelectListItem> GroupSelectList { get; set; }

        public List<Schedule> Schedules { get; set; }
    }
}

