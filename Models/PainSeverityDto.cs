using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagdyClinic.Models
{
    public class PainSeverityDto
    {
        public int Id { get; set; }
        public int PainScale { get; set; }
        public string PainLocation { get; set; }
        public string DayOrNight { get; set; }
        public string PainRadiation { get; set; }
        public DateTime PainSeverityTime { get; set; }
        public int PatientId { get; set; }
    }
}
