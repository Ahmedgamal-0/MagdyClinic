using AutoMapper;
using MagdyClinic.Models;
using MagdyClinic.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MagdyClinic.Services;

namespace MagdyClinic.Controllers
{
    [Route("Magdy/")]
    public class PatientController : Controller
    {
        IMapper _Mapper;
        IMagdyRepo _MagdyRepo;
        public PatientController(IMapper Mapper, IMagdyRepo MagdyRepo)
        {
            _Mapper = Mapper;
            _MagdyRepo = MagdyRepo;
        }
        [HttpGet("")]
        public IActionResult Test()
        {
            return Ok();
        }
        [HttpPost("Category")]
        public IActionResult AddCategory([FromBody] CategoryForCreation CategoryForCreation)
        {
            if (CategoryForCreation == null)
            {
                return BadRequest();
            }
            var Category = _Mapper.Map<Category>(CategoryForCreation);
            _MagdyRepo.AddCategory(Category);
            if (!_MagdyRepo.Save()) {
                throw new Exception("Failed to Save Category");
            }
            return Ok();
        }
        [HttpPost("Patient")]
        public IActionResult AddPatient([FromBody] PatientForCreation PatientForCreation)
        {
            if (PatientForCreation == null)
            {
                return BadRequest();
            }
            var Patient = _Mapper.Map<Patient>(PatientForCreation);
            _MagdyRepo.AddPatient(Patient);
            if (!_MagdyRepo.Save())
            {
                throw new Exception("Failed to Save Patient");
            }
            return Ok();
        }
        [HttpPost("{Id}/Question")]
        public IActionResult AddQuestion(int Id, [FromBody] QuestionForCreation QuestionForCreation)
        {
            if (QuestionForCreation == null)
            {
                return BadRequest("Sorry");
            }
            if (_MagdyRepo.GetCategory(Id) == null)
            {
                return NotFound();
            }
            var Question = _Mapper.Map<Question>(QuestionForCreation);
            Question.CategoryId = Id;
            _MagdyRepo.AddQuestion(Question, Id);
            if (!_MagdyRepo.Save())
            {
                throw new Exception("Failed to Save Question");
            }
            return Ok();
        }
        [HttpPost("{Id}/Questions")]
        public IActionResult AddQuestions(int Id, [FromBody] IEnumerable<QuestionForCreation> QuestionForCreation)
        {
            if (QuestionForCreation == null)
            {
                return BadRequest("Sorry");
            }
            if (_MagdyRepo.GetCategory(Id) == null)
            {
                return NotFound();
            }
            var Questions = _Mapper.Map<IEnumerable<Question>>(QuestionForCreation);
            foreach (var Question in Questions)
            {
                Question.CategoryId = Id;
                _MagdyRepo.AddQuestion(Question, Id);
            }
            if (!_MagdyRepo.Save())
            {
                throw new Exception("Failed to Save Question");
            }
            return Ok();
        }
        [HttpPost("Answer")]
        public IActionResult AddAnswer([FromBody] AnswerForCreation AnswerForCreation)
        {
            if (AnswerForCreation == null)
            {
                return BadRequest();
            }
            if (_MagdyRepo.GetQuestion(AnswerForCreation.QuestionId) == null)
            {
                return NotFound("Question Not Found ");
            }
            if (_MagdyRepo.GetPatient(AnswerForCreation.PatientId) == null)
            {
                return NotFound("Patient Not Found");
            }
            var Answer = _Mapper.Map<Answer>(AnswerForCreation);
            _MagdyRepo.AddAnswer(Answer);
            if (!_MagdyRepo.Save())
            {
                throw new Exception("Couldn't save your answer");
            }
            return Ok();

        }
        [HttpPost("Answers")]
        public IActionResult AddAnswers([FromBody] IEnumerable<AnswerForCreation> AnswerForCreations)
        {
            if (AnswerForCreations == null)
            {
                return BadRequest();
            }
            foreach (var Answer_ in AnswerForCreations)
            {
                if (_MagdyRepo.GetQuestion(Answer_.QuestionId) == null)
                {
                    return NotFound("Question Not Found ");
                }
                if (_MagdyRepo.GetPatient(Answer_.PatientId) == null)
                {
                    return NotFound("Patient Not Found");
                }
                var Answer = _Mapper.Map<Answer>(Answer_);
                _MagdyRepo.AddAnswer(Answer);
            }
            if (!_MagdyRepo.Save())
            {
                throw new Exception("Couldn't save your answer");
            }
            return Ok();

        }

        [HttpGet("{PatientId}/PatientAnswers")]
        public IActionResult GetPatientAnswers(int PatientId)
        {
            if (_MagdyRepo.GetPatient(PatientId) == null)
            {
                return NotFound("hi");
            }
            var AnswersToReturn = _MagdyRepo.GetAnswers(PatientId);
            return Ok(AnswersToReturn);
        }
        [HttpGet("MainQuestions")]
        public IActionResult GetMainQuestions()
        {
            var Questions = _MagdyRepo.GetMainQuestions();
            var QuestionsToReturn = new List<QuestionDto>();
            if (Questions == null)
            {
                return NotFound();
            }
            foreach (var Question_ in Questions)
            {
                QuestionsToReturn.Add(_Mapper.Map<QuestionDto>(Question_));
            }
            return Ok(QuestionsToReturn);


        }
        [HttpGet("{QuestionId}/CategoryQuestions")]
        public IActionResult GetCategoryQuestions(int QuestionId)
        {
            var Questions = _MagdyRepo.GetCategoryQuestions(QuestionId);
            var QuestionsToReturn = new List<QuestionDto>();
            if (Questions == null)
            {
                return NotFound();
            }
            foreach (var Question_ in Questions)
            {
                QuestionsToReturn.Add(_Mapper.Map<QuestionDto>(Question_));
            }
            return Ok(QuestionsToReturn);

        }
        [HttpPost("Doctor")]
        public IActionResult AddDoctor([FromBody] DoctorForCreation DoctorForCreation)
        {
            if (DoctorForCreation == null)
            {
                return BadRequest();
            }
            var Doctor = _Mapper.Map<Doctor>(DoctorForCreation);
            _MagdyRepo.AddDoctor(Doctor);
            if (!_MagdyRepo.Save())
            {
                throw new Exception("Failed to add doctor");
            }
            return Ok(Doctor);
        }
        [HttpGet("{DoctorId}/Doctor")]
        public IActionResult AddDoctor(int DoctorID)
        {
            var Doctor = _MagdyRepo.GetDoctor(DoctorID);
            if (Doctor == null)
            {
                return BadRequest();
            }
            
            return Ok(Doctor);
        }
    }
}
