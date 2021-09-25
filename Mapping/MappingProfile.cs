using AutoMapper;
using MagdyClinic.Entities;
using MagdyClinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagdyClinic.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryForCreation>().ReverseMap();
            CreateMap<Question, QuestionForCreation>().ReverseMap();
            CreateMap<Patient, PatientForCreation>().ReverseMap();
            CreateMap<Answer, AnswerForCreation>().ReverseMap();
            CreateMap<QuestionDto, Question>().ReverseMap();
            CreateMap<Doctor,DoctorForCreation>().ReverseMap();
            CreateMap<Diagnose, DiagnoseForCreation>().ReverseMap();
            CreateMap<Diagnose, DiagnoseDto>().ReverseMap();
            CreateMap<PainSeverity, PainSeverityDto>().ReverseMap();
            CreateMap<PainSeverity, PainSeverityForCreation>().ReverseMap();
            CreateMap<Slot, SlotForCreation>().ReverseMap();
            CreateMap<Slot, SlotDto>().ReverseMap();
            CreateMap<DoctorScheduleCriteria,ScheduleDto>().ReverseMap();
            CreateMap<DoctorScheduleCriteria,ScheduleForCreation>().ReverseMap();

        }
    }
}
