using DentalClinic.Core.Models;
using DentalClinic.Core.Repositories;
using DentalClinic.EF.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinic.EF.Repositories
{
	public class LabRepository : BaseRepository<Lab>, ILabRepository
	{
		public LabRepository(ApplicationDbContext context) : base(context)
		{
		}

        public object searchPatient(string patientName)
        {
            var patients = _context.Patients
            .Where(p => p.IsDeleted == 0) // Search by name
            .Where(p => p.Name.Contains(patientName)) // Search by name
            .Select(p => new { id = p.Id, name = p.Name }) // Return ID & Name
            .Take(10) // Limit results for performance
            .ToList();

            return patients;
        }

        public object DataTableAlldata(HttpRequest Request)
		{
			var pageSize = int.Parse(Request.Form["length"]);
			var skip = int.Parse(Request.Form["start"]);

			string searchValue = Request.Form["search[value]"];

			var sortColumn = Request.Form[string.Concat("columns[", Request.Form["order[0][column]"], "][name]")];
			var sortColumnDirection = Request.Form["order[0][dir]"];

			IQueryable<Lab> lab = _context.Labs.Include(p=> p.Patient).Where(x => x.IsDeleted == 0).AsQueryable();
			if (!string.IsNullOrEmpty(searchValue))
			{
				lab = lab.Where(x =>
				string.IsNullOrEmpty(searchValue) ? true :
				(x.Patient.Name.Contains(searchValue)) ||
				(x.LabName.Contains(searchValue)) ||
				(x.SampleName.Contains(searchValue)) ||
				(x.Id.ToString().Contains(searchValue)));
			}



			if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
			{
				lab = lab.OrderBy(string.Concat(sortColumn, " ", sortColumnDirection));
			}

			var data = lab.Skip(skip).Take(pageSize).ToList();


			var recordsTotal = lab.Count();

			var jsonData = new
			{
				recordsFiltered = recordsTotal,
				recordsTotal,
				data = data.Select(a => new
				{
					Id = a.Id,
					SampleName = a.SampleName,
					PatientName = a.Patient.Name,
					LabName = a.LabName,
					SampleStatus = (a.ActualReceivingDate == null) ? "تم الإرسال" : "تم الإستلام",
				})
			};
			return jsonData;
		}
	}
}
