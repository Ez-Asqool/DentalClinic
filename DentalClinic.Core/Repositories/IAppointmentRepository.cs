using DentalClinic.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinic.Core.Repositories
{
    public interface IAppointmentRepository : IBaseRepository<Appointment>
    {
		public object DataTableAlldata(HttpRequest Request);
		public object searchPatient(string patientName); 
    }
}
