using AVN.Common.Enums;

namespace AVN.Model.Entities;

public class Group : BaseEntity<Group, string>
{
    public string GroupName { get; set; }
    public Course Course { get; set; }
    public DateTime DateCreate { get; set; }
    public FormOfEducation StudingForm { get; set; }
    public AcademicDegree AcademicDegree { get; set; } 
    public TrainingPeriod TrainingPeriod { get; set; }
    public GroupType GroupType { get; set; }

    public int? AcademicYearId { get; set; }
    public virtual AcademicYear? AcademicYear { get; set; }

    public int? DirectionId { get; set; }
    public virtual Direction? Direction { get; set; }

    public virtual ICollection<Student>? Students { get; set; }
    public virtual ICollection<Order>? Orders { get; set; }
    public virtual ICollection<GroupEmployee>? GroupEmployees { get; set; }

    public Group()
    {
        GroupEmployees = new List<GroupEmployee>();
        Students = new List<Student>();
        Orders = new List<Order>();
    }

}