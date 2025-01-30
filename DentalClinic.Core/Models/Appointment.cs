using DentalClinic.Core.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinic.Core.Models
{
    public class Appointment : BaseModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = Messages.ErrorMessage), MaxLength(10)]
        public string Type { get; set; } //new, review

        public DateTime Date { get; set; }

        public DateTime TimeFrom { get; set; }

        public DateTime TimeTo { get; set; }


        public Visit Visit { get; set; }

        public int PatientId { get; set; }

        public Patient Patient { get; set; }


    }
}
