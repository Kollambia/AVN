using AVN.Common.Enums;

namespace AVN.Model.Entities;

public class Group : BaseEntity
{
    public string GroupName { get; set; }
    public Course Course { get; set; }
    public DateTime DateCreate { get; set; }
    public int DirectionId { get; set; }
    public Direction Direction { get; set; }
    public ICollection<Student> Students { get; set; }
}