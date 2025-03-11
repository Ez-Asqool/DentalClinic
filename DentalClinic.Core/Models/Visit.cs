using DentalClinic.Core.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinic.Core.Models
{
    public class Visit : BaseModel
    {
        public int Id { get; set; }
        

        public DateTime Date { get; set; }


        [Required(ErrorMessage = Messages.ErrorMessage), MaxLength(8)]
        public VisitType Type { get; set; }


        //public List<Image> Images { get; set; }


        //public string? TreatmentPlan { get; set; }


        public List<Treatment> Treatments { get; set; }


        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }


        //public int PatientId { get; set; }
        //public Patient Patient { get; set; }



    }
}
