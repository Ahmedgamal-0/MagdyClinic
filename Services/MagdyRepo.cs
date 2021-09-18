using MagdyClinic.Entities;
using MagdyClinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagdyClinic.Services
{
    public class MagdyRepo:IMagdyRepo
    {
        MagdyClinicDBContext _MagdyClinicDBContext;
        public MagdyRepo(MagdyClinicDBContext MagdyClinicDBContext)
        {
            _MagdyClinicDBContext = MagdyClinicDBContext;
        }
        public void AddCategory(Category category)
        {
            _MagdyClinicDBContext.Category.Add(category);
            
        }
        public void AddQuestion(Question Question,int CategoryId)
        {
            Category Category = _MagdyClinicDBContext.Category.FirstOrDefault(o => o.Id == CategoryId);
            if (Category != null)
            {
                _MagdyClinicDBContext.Question.Add(Question);
            }

        }
        public void AddAnswer(Answer Answer)
        {
            _MagdyClinicDBContext.Answer.Add(Answer);

        }
        public void AddPatient(Patient Patient)
        {
            _MagdyClinicDBContext.Patient.Add(Patient);

        }
        public bool Save()
        {
            return (_MagdyClinicDBContext.SaveChanges() >= 0);
        }
        public Category GetCategory(int CategoryId)
        {
            return _MagdyClinicDBContext.Category.FirstOrDefault(o => o.Id == CategoryId);
        }
        public Patient GetPatient(int PatientId)
        {
            return _MagdyClinicDBContext.Patient.FirstOrDefault(o => o.Id == PatientId);
        }
        public Question GetQuestion(int QuestionId)
        {
            return _MagdyClinicDBContext.Question.FirstOrDefault(o => o.Id == QuestionId);
        }
        public IEnumerable<PatientAnswer> GetAnswers(int PatientId)
        {
            var Answers = _MagdyClinicDBContext.Answer.Where(o => o.PatientId == PatientId).ToList();
            var Question = new Question();
            var PatientAnswers = new List<PatientAnswer>();
            var PatientAnswer = new PatientAnswer();
            foreach (var Answer in Answers)
            {
                Question=_MagdyClinicDBContext.Question.FirstOrDefault(o => o.Id == Answer.QuestionId);
                PatientAnswer.AnswerBody = Answer.AnswerBody;
                PatientAnswer.QuestionBody = Question.QuestionBody;
                PatientAnswers.Add(PatientAnswer);

            }
            return PatientAnswers;

        }
        public IEnumerable<Question> GetMainQuestions()
        {
            var Questions = _MagdyClinicDBContext.Question.Where(o => o.IsMain == true).ToList();
            return Questions;
        }
        public IEnumerable<Question> GetCategoryQuestions(int QuestionId)
        {
            var CategoryId = _MagdyClinicDBContext.Question.FirstOrDefault(o => o.Id == QuestionId).CategoryId;
            var Questions = _MagdyClinicDBContext.Question.Where(o => o.CategoryId == CategoryId&&o.IsMain!=true).ToList();
            return Questions;

        }
        public void AddDoctor(Doctor Doctor)
        {
            _MagdyClinicDBContext.Doctor.Add(Doctor);
        }
        public Doctor GetDoctor(int DoctorId)
        {
            return _MagdyClinicDBContext.Doctor.FirstOrDefault(o => o.Id == DoctorId);
        }

    }
}
