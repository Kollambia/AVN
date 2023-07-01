using AVN.Common.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace AVN.Model.Entities
{
    public class Employee : BaseEntity<Employee, string>
    {
        public string SName { get; set; }
        public string Name { get; set; }
        public string PName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public EmployeePosition Position { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public int? DepartmentId { get; set; }
        public virtual Department? Department { get; set; }

        public virtual ICollection<Subject>? Subjects { get; set; }
        public virtual ICollection<GroupEmployee>? GroupEmployees { get; set; }
        public virtual ICollection<Schedule>? Schedules { get; set; }
        public Employee()
        {
            GroupEmployees = new List<GroupEmployee>();
            Subjects = new List<Subject>();
            Schedules = new List<Schedule>();
        }

        [NotMapped]
        public string FullName
        {
            get
            {
                return this.GetFullName();
            }
        }

        public string GetFullName()
        {
            return SName + " " + Name + " " + PName;
        }
    }
}
