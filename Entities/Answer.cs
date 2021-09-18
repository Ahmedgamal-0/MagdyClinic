using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MagdyClinic.Entities
{
    public class Answer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string AnswerBody { get; set; }

        [ForeignKey("QuestionId")]
        public int QuestionId { get; set; }

        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
        public int PatientId { get; set; }
    }
}
