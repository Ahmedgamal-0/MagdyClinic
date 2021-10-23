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
        IMailingService _MailingService;
        public MagdyRepo(MagdyClinicDBContext MagdyClinicDBContext,IMailingService MailingService)
        {
            _MagdyClinicDBContext = MagdyClinicDBContext;
            _MailingService = MailingService;
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
        public void AddDiagnose(Diagnose Diagnose)
        {
            _MagdyClinicDBContext.Diagnose.Add(Diagnose);
        }
        public Diagnose GetDiagnose(int PatientId)
        {
            return _MagdyClinicDBContext.Diagnose.FirstOrDefault(o => o.PatientId == PatientId);
        }

        public void AddPainSeverity(PainSeverity PainSeverity)
        {
            _MagdyClinicDBContext.PainSeverity.Add(PainSeverity);
        }

        public IEnumerable<PainSeverity> GetPainSeverity(int PatientId)
        {
            var PainSeverities = _MagdyClinicDBContext.PainSeverity.Where(o => o.PatientId == PatientId).ToList();
            return PainSeverities;
        }

        public void AddScheduleCriteria(DoctorScheduleCriteria DoctorScheduleCriteria)
        {
            _MagdyClinicDBContext.DoctorScheduleCriteria.Add(DoctorScheduleCriteria);   
        }
        public IEnumerable<Slot> AddSlots(DoctorScheduleCriteria DoctorScheduleCriteria)
        {
            string[] StartTime = DoctorScheduleCriteria.StartTime.ToString().Split('T');
            TimeSpan TimeDifferance = DoctorScheduleCriteria.EndTime - DoctorScheduleCriteria.StartTime;
            int Hours = TimeDifferance.Hours;
            int Minutes = TimeDifferance.Minutes;
            int Slots = ((Hours * 60) + Minutes) / DoctorScheduleCriteria.SlotDuration;
            DateTime SlotStart = DoctorScheduleCriteria.StartTime;
            
            var FinalSlots = new List<Slot>();
            
            for (int i = 0; i < Slots; i++)
            {
                var NewSlot = new Slot();
                NewSlot.DateTime = SlotStart;
                NewSlot.IsTaken = false;
                NewSlot.DoctorScheduleCriteriaId = DoctorScheduleCriteria.Id;
                SlotStart = NewSlot.DateTime.AddHours(DoctorScheduleCriteria.SlotDuration / 60);
                SlotStart.AddMinutes(DoctorScheduleCriteria.SlotDuration % 60);
                FinalSlots.Add(NewSlot);
                _MagdyClinicDBContext.Slot.Add(NewSlot);
                Save();
                

            }
            /*foreach (var sl in FinalSlots)
            {
                _MagdyClinicDBContext.Add(sl);
            }
                Save();
            */
            return FinalSlots;
        }

        public IEnumerable<DoctorScheduleCriteria> GetScheduleCriteria(int DoctorId)
        {
            var DoctorSchedules = _MagdyClinicDBContext.DoctorScheduleCriteria.Where(o => o.DoctorId == DoctorId).ToList();
            return DoctorSchedules;
        }

        public void AddSlot(Slot Slot)
        {
            _MagdyClinicDBContext.Slot.Add(Slot);
        }

        public IEnumerable<Slot> GetSlots(int PatientId)
        {
            var Slots = _MagdyClinicDBContext.Slot.Where(o => o.PatientId == PatientId);
            return Slots;
        }
        public void ReserveSlot(int PatientId, int SlotId)
        {
            var slot = _MagdyClinicDBContext.Slot.FirstOrDefault(o => o.Id == SlotId);
            slot.PatientId = PatientId;
            slot.IsTaken = true;
            var Patient = GetPatient(PatientId);
            _MailingService.SendEmailAsync(Patient.Email, "Slot Reservation", "Hi " + Patient.Name + " You have reservered slot in : " + slot.DateTime + " Please Try to be there atleast 5 mins before session");
            _MagdyClinicDBContext.Slot.Update(slot);
            
        }

        public Doctor Authenticate(string DoctorName, string Password)
        {
            if (string.IsNullOrEmpty(DoctorName) || string.IsNullOrEmpty(Password))
                return null;

            var Doctor = _MagdyClinicDBContext.Doctor.SingleOrDefault(x => x.Name == DoctorName && x.Password==Password);

            // check if username exists
            if (Doctor == null)
                return null;

            // check if password is correct
            // if (!VerifyPasswordHash(password, admin.PasswordHash, admin.PasswordSalt))
            //   return null;

            // authentication successful
            return Doctor;
        }
    }
}
