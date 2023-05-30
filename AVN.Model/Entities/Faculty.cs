namespace AVN.Model.Entities;

public class Faculty : BaseEntity<Faculty, int>
{
    public string FacultyName { get; set; }
    public string? FacultyShortName { get; set; }
    public string DeanName { get; set; }

    public virtual ICollection<Department> Departments { get; set; }

    public Faculty() 
    { 
        Departments = new List<Department>();
    }

}