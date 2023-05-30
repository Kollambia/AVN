using AVN.Common.Enums;
using Microsoft.AspNetCore.Identity;

namespace AVN.Model.Entities
{
    public class AppUser : IdentityUser
    {
        public string? SName { get; set; }

        public string? Name { get; set; }

        public string? PName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public FormOfEducation? StudingForm { get; set; }

        public EducationalLine? EducationalLine { get; set; }

        public AcademicDegree? AcademicDegree { get; set; }

        public string? GradeBookNumber { get; set; }

        public StudentStatus? Status { get; set; }

        public Gender? Gender { get; set; }

        public Citizenship? Citizenship { get; set; }

        public string? Address { get; set; }

        public string? PhoneNumber { get; set; }

        public int? FacultyId { get; set; }

        public int? DepartmentId { get; set; }

        public int? DirectionId { get; set; }

        public int? GroupId { get; set; }

        public virtual Group? Group { get; set; }
    }
}
