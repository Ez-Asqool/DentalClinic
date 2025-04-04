﻿using DentalClinic.Core.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinic.Core.Models
{
    public class Patient : BaseModel
    {
        public int Id { get; set; }


        [Required(ErrorMessage = Messages.ErrorMessage), MaxLength(50)]
        public string Name { get; set; }


        [Required(ErrorMessage = Messages.ErrorMessage)]
        public DateTime DateOfBirth { get; set; }


        [Required(ErrorMessage = Messages.ErrorMessage), MaxLength(8)]
        public Gender Gender { get; set; }


        [Required(ErrorMessage = Messages.ErrorMessage)]
        public int Age { get; set; }


        [MaxLength(50)]
        public string? Job { get; set; }


        [Required(ErrorMessage = Messages.ErrorMessage), MaxLength(50)]
        public string Address { get; set; }


        [Required(ErrorMessage = Messages.ErrorMessage), MaxLength(20)]
        public string Firstphone { get; set; }


        [MaxLength(20)]
        public string? Secondphone { get; set; }


        [MaxLength(20)]
        public string? PersonalIDNumber { get; set; }


        [MaxLength(50)]
        public string? InsuranceCardNumber { get; set; }


        [MaxLength(1000)]
        public string? Notes { get; set; }


        //public int DoctorId { get; set; }
        //public Doctor Doctor { get; set; }


        public List<Appointment> Appointments { get; set; }

        //public List<Visit> Visits { get; set; }

        public List<Lab> Labs { get; set; }

    }
}
