using System.ComponentModel.DataAnnotations;
using AVN.Common.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace AVN.Model.Entities
{
    public class Employee : BaseEntity<Employee, string>
    {
        [Display(Name = "Фамилия")]
        public string SName { get; set; }
        [Display(Name = "Имя")]
        public string Name { get; set; }
        [Display(Name = "Отчество")]
        public string PName { get; set; }
        [Display(Name = "Дата рождения")]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "Почта")]
        public string Email { get; set; }
        [Display(Name = "Пол")]
        public Gender Gender { get; set; }
        [Display(Name = "Должность")]
        public EmployeePosition Position { get; set; }
        [Display(Name = "Адрес")]
        public string Address { get; set; }
        [Display(Name = "Номер")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Кафедра")]
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
