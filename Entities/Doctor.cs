using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MagdyClinic.Entities
{
    public class Doctor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Occupation { get; set; }
        public int Age { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public IEnumerable<Patient> Patients { get; set; }
    }
}
