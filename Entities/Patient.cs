using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MagdyClinic.Entities
{
    public class Patient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Occupation { get; set; }
        public int Age { get; set; }
        public int Length { get; set; }
        public int Weight { get; set; }
        public string BloodPressure { get; set; }
        public string RBS { get; set; }
    }
}
