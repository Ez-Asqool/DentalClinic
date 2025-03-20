using DentalClinic.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinic.Core.Repositories
{
	public interface ILabRepository : IBaseRepository<Lab>
	{

		public object DataTableAlldata(HttpRequest Request);
        public object searchPatient(string patientName);
    }
}
