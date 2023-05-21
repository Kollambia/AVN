using AutoMapper;
using AVN.Common.Enums;
using AVN.Model.Entities;
using AVN.Models.ModelVM;

namespace AVN.Automapper
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile() 
        {
            CreateMap<Faculty, FacultyVM>().ReverseMap();
            CreateMap<Department, DepartmentVM>().ReverseMap();
            CreateMap<Direction, DirectionVM>().ReverseMap();
        }
    }
}
