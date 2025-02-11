using DentalClinic.Core.Models;
using DentalClinic.Core.Repositories;
using DentalClinic.EF.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinic.EF.Repositories
{
	public class DoctorRepository : BaseRepository<Doctor>, IDoctorRepository
	{
		public DoctorRepository(ApplicationDbContext context) : base(context)
		{
		}
		
		public object DataTableAlldata(HttpRequest Request)
		{
			var pageSize = int.Parse(Request.Form["length"]);
			var skip = int.Parse(Request.Form["start"]);

			string searchValue = Request.Form["search[value]"];

			var sortColumn = Request.Form[string.Concat("columns[", Request.Form["order[0][column]"], "][name]")];
			var sortColumnDirection = Request.Form["order[0][dir]"];

			IQueryable<Doctor> gaza = _context.Doctors.Where(x => x.IsDeleted == 0).AsQueryable();
			if (!string.IsNullOrEmpty(searchValue))
			{
				gaza = gaza.Where(x =>
				string.IsNullOrEmpty(searchValue) ? true :
				(x.Name.Contains(searchValue)) ||
				(x.Age.ToString().Contains(searchValue)));
			}


			
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                gaza = gaza.OrderBy(string.Concat(sortColumn, " ", sortColumnDirection));
            }
            
			var data = gaza.Skip(skip).Take(pageSize).ToList();


			var recordsTotal = gaza.Count();

			var jsonData = new
			{
				recordsFiltered = recordsTotal,
				recordsTotal,
				data
			};
			return jsonData;
		}
	}
}
