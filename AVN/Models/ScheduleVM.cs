using AVN.Common.Enums;
using AVN.Model.Entities;

namespace AVN.Models
{
    public class ScheduleVM : BasicVM<int>
    {
        public DaysOfTheWeek Days { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string GroupId { get; set; }
        public virtual Group Group { get; set; }
        public string SubjectId { get; set; }
        public virtual Subject Subject { get; set; }
        public string EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
