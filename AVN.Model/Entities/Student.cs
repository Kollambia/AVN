using AVN.Common.Enums;

namespace AVN.Model.Entities;

public class Student : BaseEntity<Student, string>
{
    public string SName { get; set; }
    public string Name { get; set; }
    public string PName { get; set; }
    public StudentStatus Status { get; set; }
    public DateTime DateOfBirth { get; set; }
    public EducationalLine EducationalLine { get; set; }
    public string GradeBookNumber { get; set; }
    public Gender Gender { get; set; }
    public Citizenship Citizenship { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public int RecruitmentYear{ get; set; }
    public bool IsHasDebt { get; set; }

    public int GroupId { get; set; }
    public virtual Group? Group { get; set; }

    public virtual ICollection<StudentPayment>? StudentPayments { get; set; }
    public virtual ICollection<Order>? Orders { get; set; }
    public virtual ICollection<StudentMovement>? StudentMovements { get; set; }

    public Student()
    {
        StudentPayments = new List<StudentPayment>();
        Orders = new List<Order>();
        StudentMovements = new List<StudentMovement>();
    }

}