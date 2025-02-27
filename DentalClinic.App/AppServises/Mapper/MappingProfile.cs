using AutoMapper;
using DentalClinic.Core.Models;
using DentalClinic.Core.ViewModels.AppointmentVMs;
using DentalClinic.Core.ViewModels.ClinicVMs;
using DentalClinic.Core.ViewModels.DoctorVMs;
using DentalClinic.Core.ViewModels.PatientVMs;
using DentalClinic.Core.ViewModels.RoomVMs;

namespace DentalClinic.App.AppServises.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Doctor
            CreateMap<Room, DentalClinic.Core.ViewModels.DoctorVMs.IndexVM>();
            CreateMap<AddDoctorVM, Doctor>();
            CreateMap<Doctor, UpdateDoctorVM>().ReverseMap();    
            CreateMap<Doctor, DetailsDoctorVM>();

            //Room
            CreateMap<AddRoomVM, Room>();
            CreateMap<Room, UpdateRoomVM>().ReverseMap();
            CreateMap<Room, DetailsRoomVM>();

            //Clinic
            CreateMap<Clinic, DentalClinic.Core.ViewModels.ClinicVMs.IndexVM>().ReverseMap();

            //Patient
            CreateMap<AddPatientVM, Patient>();
            CreateMap<Patient, UpdatePatientVM>().ReverseMap();
            CreateMap<Patient, DetailsPatientVM>();

            //Appointment
            CreateMap<AddAppointmentVM, Appointment>();
            CreateMap<Appointment, TodayAppointmentVM>();

        }
    }
}
