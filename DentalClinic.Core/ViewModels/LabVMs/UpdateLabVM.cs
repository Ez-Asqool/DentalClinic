using DentalClinic.Core.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinic.Core.ViewModels.LabVMs
{
	public class UpdateLabVM
	{
        public int Id { get; set; }


        [Required(ErrorMessage = Messages.ErrorMessage), MaxLength(300)]
        public string LabName { get; set; }


        [Required(ErrorMessage = Messages.ErrorMessage), MaxLength(300)]
        public string SampleName { get; set; }


        [Required(ErrorMessage = Messages.ErrorMessage)]
        public int Quantity { get; set; }


        [Required(ErrorMessage = Messages.ErrorMessage)]
        public SampleType SampleType { get; set; }


        [MaxLength(1500)]
        public string? Notice { get; set; }


        [Required(ErrorMessage = Messages.ErrorMessage)]
        public DateTime SendingDate { get; set; }


        [Required(ErrorMessage = Messages.ErrorMessage)]
        public DateTime? ReceivingDate { get; set; }


        public DateTime? ActualReceivingDate { get; set; }


        [Required(ErrorMessage = Messages.ErrorMessage)]
        public double Price { get; set; }


        [Required(ErrorMessage = Messages.ErrorMessage)]
        public int PatientId { get; set; }


        public string? PName { get; set; }
    }
}
