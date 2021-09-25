using MagdyClinic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagdyClinic.Models
{
    public class ScheduleForCreation
    {
       
        public string Day { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int SlotDuration { get; set; }
        public int DoctorId { get; set; }
        
    }
}
