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
        }
    }
}
