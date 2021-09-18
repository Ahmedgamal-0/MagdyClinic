using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MagdyClinic.Entities
{
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string QuestionBody { get; set; }
        public Boolean IsMain { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { set; get; }
        public int CategoryId { set; get; }
    }
}
