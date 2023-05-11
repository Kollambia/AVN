namespace AVN.Model.Entities;

public class Faculty : BaseEntity
{
    public string FacultyName { get; set; }

    public ICollection<Group> Groups { get; set; }
}