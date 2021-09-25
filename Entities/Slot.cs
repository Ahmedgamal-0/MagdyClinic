using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MagdyClinic.Entities
{
    public class Slot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public Boolean IsTaken { get; set; }
        public int PatientId { get; set; }

        [ForeignKey("DoctorScheduleCriteriaId")]
        public DoctorScheduleCriteria DoctorScheduleCriteria { get; set; }
        public int DoctorScheduleCriteriaId { get; set; }

    }
}
