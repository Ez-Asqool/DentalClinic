using AutoMapper;
using DentalClinic.Core.Models;
using DentalClinic.Core.ViewModels.DoctorVMs;

namespace DentalClinic.App.AppServises.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Doctor
            CreateMap<Room, IndexVM>();
            CreateMap<AddDoctorVM, Doctor>();
            CreateMap<Doctor, UpdateDoctorVM>().ReverseMap();    
            CreateMap<Doctor, DetailsDoctorVM>();
        }
    }
}
