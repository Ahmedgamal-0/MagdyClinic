using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagdyClinic.Entities
{
    public class MagdyClinicDBContext : DbContext
    {
        public MagdyClinicDBContext(DbContextOptions<MagdyClinicDBContext> options) : base(options)
        {
            Database.Migrate();

        }
        public DbSet<Patient> Patient { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>().ToTable("Patient");
        }
    }
}


