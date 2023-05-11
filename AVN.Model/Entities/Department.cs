namespace AVN.Model.Entities;

public class Department : BaseEntity
{
    public string DepartmentName { get; set; }

    public ICollection<Group> Groups { get; set; }
}