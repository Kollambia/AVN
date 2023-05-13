namespace AVN.Model.Entities;

public class Department : BaseEntity
{
    public string DepartmentName { get; set; }
    public string DepartmentShortName { get; set; }
    public int FacultyId { get; set; }
    public Faculty Faculty { get; set; }
    public ICollection<Employee> Employees { get; set; }
    public ICollection<Direction> Directions { get; set; }
}

