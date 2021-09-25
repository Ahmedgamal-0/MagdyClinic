using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagdyClinic.Models
{
    public class DiagnoseDto
    {
        public int Id { get; set; }
        public string DoctorDiagnose { get; set; }
        public int SessionsNumber { get; set; }
        public string Comments { get; set; }
        public DateTime DiagnoseDate { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
    }
}
