using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MagdyClinic.Entities
{
    public class Diagnose
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string DoctorDiagnose { get; set; }
        public int SessionsNumber { get; set; }
        public string Comments { get; set; }
        public DateTime DiagnoseDate { get; set; }

        [ForeignKey("DoctorId")]
        public Doctor Doctor { get; set; }
        public int DoctorId { get; set; }

       /* [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
        public int PatientId { get; set; }
       */
    }
}
