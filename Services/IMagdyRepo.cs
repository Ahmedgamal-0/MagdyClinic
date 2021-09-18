using MagdyClinic.Entities;
using MagdyClinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagdyClinic.Services
{
    public interface IMagdyRepo
    {
        public void AddCategory(Category category);
        public void AddQuestion(Question Questions,int CategoryId);
        public void AddPatient(Patient Patient);
        public void AddAnswer(Answer Answer);
        public void AddDoctor( Doctor Doctor);
        public Category GetCategory(int CategoryId);
        public Patient GetPatient(int PatientId);
        public Question GetQuestion(int QuestionId);
        public Doctor GetDoctor(int DoctorId);
        public IEnumerable<PatientAnswer> GetAnswers(int PatientId);
        public IEnumerable<Question> GetMainQuestions();
        public IEnumerable<Question> GetCategoryQuestions(int QuestionId);
        public bool Save();
    }
}
