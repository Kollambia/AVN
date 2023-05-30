namespace AVN.Model.Entities;

public class Department : BaseEntity<Department, int>
{
    public string DepartmentName { get; set; }
    public string DepartmentShortName { get; set; }

    public int? FacultyId { get; set; }
    public virtual Faculty Faculty { get; set; }

    public virtual ICollection<Employee> Employees { get; set; }
    public virtual ICollection<Direction> Directions { get; set; }
    public virtual ICollection<Subject> Subjects { get; set; }

    public Department() 
    {
        Employees = new List<Employee>();
        Directions = new List<Direction>();
        Subjects = new List<Subject>();
    }
}

