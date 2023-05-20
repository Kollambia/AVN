namespace AVN.Model.Entities;

public class Faculty : BaseEntity
{
    public string FacultyName { get; set; }
    public string? FacultyShortName { get; set; }
    public string DeanName { get; set; }
    public ICollection<Department> Departments { get; set; }

}