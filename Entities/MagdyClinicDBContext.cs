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
        public DbSet<Category> Category { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Answer> Answer { get; set; }
        public DbSet<Doctor> Doctor { get; set; }
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>().ToTable("Patient");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Question>().ToTable("Question");
            modelBuilder.Entity<Answer>().ToTable("Answer");
            modelBuilder.Entity<Doctor>().ToTable("Doctor");
          

           
        }
    }
}


