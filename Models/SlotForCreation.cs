using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagdyClinic.Models
{
    public class SlotForCreation
    {
        public DateTime DateTime { get; set; }
        public Boolean IsTaken { get; set; }
        public int PatientId { get; set; }
        public int DoctorScheduleCriteriaId { get; set; }
    }
}
