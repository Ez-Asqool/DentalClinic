using DentalClinic.Core.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinic.Core.ViewModels.PatientVMs
{
    public class UpdatePatientVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = Messages.ErrorMessage), MaxLength(50)]
        public string Name { get; set; }


        [Required(ErrorMessage = Messages.ErrorMessage)]
        public DateTime DateOfBirth { get; set; }


        [Required(ErrorMessage = Messages.ErrorMessage)]
        public Gender Gender { get; set; }


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
    }
}
