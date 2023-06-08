using AutoMapper;
using AVN.Common.Enums;
using AVN.Model.Entities;
using AVN.Models;

namespace AVN.Automapper
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile() 
        {
            CreateMap<Faculty, FacultyVM>().ReverseMap();
            CreateMap<Department, DepartmentVM>().ReverseMap();
            CreateMap<Direction, DirectionVM>().ReverseMap();
            CreateMap<Group, GroupVM>().ReverseMap();
            CreateMap<Student, StudentVM>().ReverseMap();
            CreateMap<StudentPayment, StudentPaymentVM>().ReverseMap();
            CreateMap<StudentPaymentDetail, StudentPaymentDetailVM>().ReverseMap();
            CreateMap<AcademicYear, AcademicYearVM>().ReverseMap();
            CreateMap<MovementType, MovementTypeVM>().ReverseMap();
            CreateMap<OrderType, OrderTypeVM>().ReverseMap();
            CreateMap<Subject, SubjectVM>().ReverseMap();
            CreateMap<Employee, EmployeeVM>().ReverseMap();
        }
    }
}
