using DentalClinic.Core.Models;
using DentalClinic.Core.Repositories;
using DentalClinic.EF.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinic.EF.Repositories
{
    public class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(ApplicationDbContext context) : base(context)
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

			IQueryable<Appointment> appointments = _context.Appointments.Where(x => x.IsDeleted == 0).AsQueryable().Include(x => x.Visit).Include(x => x.Patient).Include(x => x.Doctor);
			if (!string.IsNullOrEmpty(searchValue))
			{
				appointments = appointments.Where(x =>
				string.IsNullOrEmpty(searchValue) ? true :
				(x.Doctor.Name.Contains(searchValue)) ||
				(x.Patient.Name.ToString().Contains(searchValue)) ||
				(x.Type.ToString().Contains(searchValue)) ||
				(x.Date.ToString().Contains(searchValue)) ||
				(x.TimeFrom.ToString().Contains(searchValue)));

			}



			if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
			{
				appointments = appointments.OrderBy(string.Concat(sortColumn, " ", sortColumnDirection));
			}

			var data = appointments.Skip(skip).Take(pageSize).ToList();


			var recordsTotal = appointments.Count();

			var jsonData = new
			{
				recordsFiltered = recordsTotal,
				recordsTotal,
				data = data.Select(a => new
				{
					Id = a.Id,
					PatientName = a.Patient != null ? a.Patient.Name : "N/A",  // Ensure this exists in JSON
					DoctorName = a.Doctor != null ? a.Doctor.Name : "N/A",
					Date = a.Date.ToString("yyyy-MM-dd"),
					TimeFrom = a.TimeFrom.ToString("HH:mm"),
					TimeTo = a.TimeTo.ToString("HH:mm"),
					Type = a.Type.ToString(),
					VisitStatus = a.Visit != null ? "تمت" : "لم تتم"
				})
			};
			return jsonData;
		}
	}
}
