using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagdyClinic.Models
{
    public class AnswerForCreation
    {
        public string AnswerBody { get; set; }
        public int QuestionId { get; set; }
        public int PatientId { get; set; }
    }
}
