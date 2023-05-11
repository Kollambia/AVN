namespace AVN.Model.Entities;

public class Department : BaseEntity
{
    public string DepartmentName { get; set; }
    public int FacultyId { get; set; }
    public Faculty Faculty { get; set; }
    public ICollection<Group> Groups { get; set; }
}

