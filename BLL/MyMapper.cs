using AutoMapper;
using Dal_Repository.models;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MyMapper : Profile
    {
          public MyMapper()
            {
            CreateMap<AppointmentsDto, Appointment>() // מיפוי מה-DTO ל-Appointment
                       .ForMember(dest => dest.Doctor, opt => opt.MapFrom(src => src.Doctor))
                       .ForMember(dest => dest.Patient, opt => opt.MapFrom(src => src.Patient))
                       .ForMember(dest => dest.Medicine, opt => opt.MapFrom(src => src.Medicine));
            CreateMap<Appointment, AppointmentsDto>()
                    .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.DoctorNavigation.Name))
                    .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.PatientNavigation.Name))
                    .ForMember(dest => dest.MedicineName, opt => opt.MapFrom(src => src.MedicineNavigation.Name));
        }
    }
}
