﻿using AutoMapper;
using DentalClinic.Core.Models;
using DentalClinic.Core.ViewModels.AppointmentVMs;
using DentalClinic.Core.ViewModels.ClinicVMs;
using DentalClinic.Core.ViewModels.DoctorVMs;
using DentalClinic.Core.ViewModels.FinanceVMs;
using DentalClinic.Core.ViewModels.LabVMs;
using DentalClinic.Core.ViewModels.PatientVMs;
using DentalClinic.Core.ViewModels.RoomVMs;
using DentalClinic.Core.ViewModels.VisitVMs;

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

            //Visit
            CreateMap<Visit, DetailsVisitVM>();

            //Finance
            CreateMap<AddFinanceVM, Finance>();
            CreateMap<Finance, UpdateFinanceVM>().ReverseMap();

            //Lab
            CreateMap<AddLabVM, Lab>(); 
            CreateMap<Lab, DetailsLabVM>();
            CreateMap<Lab, UpdateLabVM>().ReverseMap();

        }
    }
}
