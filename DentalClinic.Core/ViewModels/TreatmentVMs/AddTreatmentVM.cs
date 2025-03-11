using DentalClinic.Core.Consts;
using DentalClinic.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinic.Core.ViewModels.TreatmentVMs
{
    public class AddTreatmentVM
    {
        [Required(ErrorMessage = Messages.ErrorMessage)]
        public string PlaceOfTreatment { get; set; }

        [Required(ErrorMessage = Messages.ErrorMessage), MaxLength(50)]
        public string TypeOfTreatment { get; set; }

        [Required(ErrorMessage = Messages.ErrorMessage), MaxLength(1000)]
        public string? Notice { get; set; }

        [Required(ErrorMessage = Messages.ErrorMessage)]
        [Range(0, 99999, ErrorMessage = "Price must be between 0 and 99999.")]
        public double Price { get; set; }

        [Required(ErrorMessage = Messages.ErrorMessage)]
        [Range(0, 99999, ErrorMessage = "Discount must be between 0 and 99999.")]
        public double? Discount { get; set; }

        [Required(ErrorMessage = Messages.ErrorMessage)]
        [Range(0, 99999, ErrorMessage = "Total must be between 0 and 99999.")]
        public double Total { get; set; }

        public List<IFormFile>? Images { get; set; }

        public int AppointmentId { get; set; }

        public Appointment? Appointment { get; set; }    
    }
}
