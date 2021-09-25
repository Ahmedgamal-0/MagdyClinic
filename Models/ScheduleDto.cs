using MagdyClinic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagdyClinic.Models
{
    public class ScheduleDto
    {
        public int Id { get; set; }
        public string Day { get; set; }
        public ICollection<Slot> Slots { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Duration { get; set; }
        public int DoctorId { get; set; }
    }
}
