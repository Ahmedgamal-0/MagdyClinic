using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MagdyClinic.Entities
{
    public class PainSeverity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PainScale { get; set; }
        public string PainLocation { get; set; }
        public string DayOrNight { get; set; }
        public string PainRadiation { get; set; }
        public DateTime PainSeverityTime { get; set; }

        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
        public int PatientId { get; set; }

    }
}
