using AutoMapper;
using MagdyClinic.Models;
using MagdyClinic.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MagdyClinic.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using MagdyClinic.Helpers;
using Microsoft.Extensions.Options;

namespace MagdyClinic.Controllers
{
    [Route("Magdy/")]
    public class PatientController : Controller
    {
        IMapper _Mapper;
        IMagdyRepo _MagdyRepo;
        private readonly JWT _JWT;
        public PatientController(IMapper Mapper, IMagdyRepo MagdyRepo,IOptions<JWT>JWT)
        {
            _Mapper = Mapper;
            _MagdyRepo = MagdyRepo;
            _JWT = JWT.Value;
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


        [HttpPost("{PatientId}/Diagnose")]
        public IActionResult AddDiagnose(int PatientId,[FromBody]DiagnoseForCreation DiagnoseForCreation)
        {
            if (_MagdyRepo.GetDoctor(DiagnoseForCreation.DoctorId)==null)
            {
                return NotFound();
            }
            if (_MagdyRepo.GetPatient(PatientId) == null)
            {
                return NotFound();
            }
            var Diagnose = _Mapper.Map<Diagnose>(DiagnoseForCreation);
            Diagnose.PatientId = PatientId;
            _MagdyRepo.AddDiagnose(Diagnose);
            if (!_MagdyRepo.Save())
            {
                throw new Exception("Failed To Save Diagnose");
            }
            return Ok();

        }

        [HttpGet("{PatientId}/Diagnose")]
        public IActionResult GetDiagnose(int PatientId)
        {
            Diagnose Diagnose = _MagdyRepo.GetDiagnose(PatientId);
            if (Diagnose == null)
            {
                return NotFound();
            }
            var DiagnoseToReturn = _Mapper.Map<DiagnoseDto>(Diagnose);
            
            return Ok(DiagnoseToReturn);

        }

        [HttpPost("{DoctorId}/DoctorScheduleCriteria")]
        public IActionResult AddDoctorSchedule(int DoctorId,[FromBody]ScheduleForCreation ScheduleForCreation)
        {
            if (ScheduleForCreation == null)
            {
                return BadRequest();
            }
            if (_MagdyRepo.GetDoctor(DoctorId) == null)
            {
                return NotFound();
            }
            var Schedule = _Mapper.Map<DoctorScheduleCriteria>(ScheduleForCreation);
            _MagdyRepo.AddScheduleCriteria(Schedule);
            if (!_MagdyRepo.Save())
            {
                throw new Exception("Failed to save");
            }
            var sl=_MagdyRepo.AddSlots(Schedule);
            return Ok() ;
        }

        [HttpGet("{DoctorId}/DoctorScheduleCriteria")]
        public IActionResult GetScheduleCriteria(int DoctorId)
        {
            if (_MagdyRepo.GetDoctor(DoctorId)==null)
            {
                return NotFound();
            }
            var DoctorSchedule = _MagdyRepo.GetScheduleCriteria(DoctorId);
            var DS = new List<ScheduleDto>();
            var ScheduleToReturn = new ScheduleDto();
            foreach (var Schedule in DoctorSchedule)
            {
                ScheduleToReturn = _Mapper.Map<ScheduleDto>(Schedule);
                DS.Add(ScheduleToReturn);
            }
            return Ok(DS);
        }

        [HttpPost("{PatientId}/PainSeverity")]
        public IActionResult AddPatientSeverity([FromBody] PainSeverityForCreation PainSeverityForCreation,int PatientId)
        {
            if (PainSeverityForCreation == null)
            {
                return BadRequest();
            }
            if (_MagdyRepo.GetPatient(PatientId) == null)
            {
                return NotFound();
            }
            var PainSeverity = _Mapper.Map<PainSeverity>(PainSeverityForCreation);
            _MagdyRepo.AddPainSeverity(PainSeverity);
            if (!_MagdyRepo.Save())
            {
                throw new Exception("Failed To Save");
            }
            return Ok();

        }

        [HttpGet("{PatientId}/PainSeverity")]
        public IActionResult GetPatientSeverity( int PatientId)
        {
           
            if (_MagdyRepo.GetPatient(PatientId) == null)
            {
                return NotFound();
            }
            var PainSeverity = _MagdyRepo.GetPainSeverity(PatientId);
            var PainSeverityList = new List<PainSeverityDto>();
            foreach(var Pain in PainSeverity)
            {
                PainSeverityList.Add(_Mapper.Map<PainSeverityDto>(Pain));
            }
            
            return Ok(PainSeverityList);

        }

        [HttpGet("{PatientId}/SlotReserevation/{SlotId}")]
        public IActionResult ReserveSlot(int PatientId,int SlotId)
        {
            if (_MagdyRepo.GetPatient(PatientId)==null)
            {
                return NotFound();
            }
            _MagdyRepo.ReserveSlot(PatientId, SlotId);
            if (!_MagdyRepo.Save())
            {
                throw new Exception("Failed ot update");
            }
            return Ok();
        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserLoginModel model) //Get Token - login
        {
            var Doctor = _MagdyRepo.Authenticate(model.UserName, model.Password);

            if (Doctor == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenString = GenerateJwtToken(Doctor);

            // return basic user info and authentication token
            return Ok(new
            {
                // Id = admin.Id,
                // Username = admin.UserName,
                Token = tokenString
            });
        }

        public string GenerateJwtToken(Doctor Doctor)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_JWT.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                /* Subject = new ClaimsIdentity(new[] {
                     new Claim("id", admin.Id.ToString()),
                     new Claim("UserName", admin.UserName.ToString())

                 }),*/
                Expires = DateTime.UtcNow.AddMinutes(_JWT.DurationInMins),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        /*  [HttpPost("Send")]
          public async Task<IActionResult> SendEmail([FromForm] EmailDto email)
          {
              await _mailingService.SendEmailAsync(email.ToEmail, email.Subject, email.Body, email.Attachments);
              return Ok();
          }*/

        [HttpPost("DecodeJwt")]
        public IActionResult DecodeJwt([FromBody] Decoding jwtEncodedString)
        {
            var token = jwtEncodedString;
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token.Token);
            return Ok(jwtSecurityToken.Claims);

            //return Ok(jwtSecurityToken.Claims.First(claim => claim.Type == "UserName").Value);
        }

    }
}
