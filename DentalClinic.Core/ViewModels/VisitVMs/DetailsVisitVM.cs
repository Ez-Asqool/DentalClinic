using DentalClinic.Core.Consts;
using DentalClinic.Core.Models;
using DentalClinic.Core.ViewModels.TreatmentVMs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinic.Core.ViewModels.VisitVMs
{
    public class DetailsVisitVM
    {

        public int Id { get; set; }

        public DateTime Date { get; set; }

        public VisitType Type { get; set; }

        public List<Treatment> Treatments { get; set; }

        public string PatientName { get; set; }

        public string DoctorName { get; set; }

        public int AppointmentId { get; set; }

        public DateTime AppointmentDate { get; set; }

        public List<int> allVisitsIds { get; set; }


    }
}
