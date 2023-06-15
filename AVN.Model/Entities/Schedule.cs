using AVN.Common.Enums;

namespace AVN.Model.Entities
{
    public class Schedule : BaseEntity<Schedule, int>
    {
        public DaysOfTheWeek Days { get; set; }
        public string Time { get; set; }
        public string GroupId { get; set; }
        public virtual Group Group { get; set; }
        public string SubjectId { get; set; }
        public virtual Subject Subject { get; set; }
        public string EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
