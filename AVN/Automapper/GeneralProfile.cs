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
        }
    }
}
