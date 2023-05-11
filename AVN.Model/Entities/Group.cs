namespace AVN.Model.Entities;

public class Group : BaseEntity
{
    public string GroupName { get; set; }
    public int DepartmentId { get; set; }
    public Department Departments { get; set; }
    public ICollection<Student> Students { get; set; }
}