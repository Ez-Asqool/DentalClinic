using DentalClinic.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinic.Core.Repositories
{
	public interface IDoctorRepository : IBaseRepository<Doctor>
	{
		public object DataTableAlldata(HttpRequest Request);
	}
}
