using DentalClinic.Core.Consts;
using DentalClinic.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinic.Core.ViewModels.AppointmentVMs
{
    public class AddAppointmentVM
    {
        public int PatientId { get; set; }

        public VisitType Type { get; set; }

        public DateTime Date { get; set; }

        public DateTime TimeFrom { get; set; }

        public int DoctorId { get; set; }
        public IEnumerable<Doctor>? Doctors { get; set; }
    }
}
