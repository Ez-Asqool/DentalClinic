using DentalClinic.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinic.EF.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }
        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clinic>().HasData(
                new Clinic { Id = 1, Name = "عيادة ... لطب وجراحة الفم والأسنان", Phone = "0595****", Address = "....", LogoName = "ADD LOGO" }
            );

            base.OnModelCreating(modelBuilder);
        }
        */
        public DbSet<Room> Rooms { get; set; }
		public DbSet<Clinic> Clinic { get; set; }
		public DbSet<Visit> Visits { get; set; }
		public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Treatment> Treatments { get; set; }    
        


    }
}
