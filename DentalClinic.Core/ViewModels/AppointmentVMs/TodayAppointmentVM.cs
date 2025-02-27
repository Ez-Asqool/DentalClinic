using DentalClinic.Core.Consts;
using DentalClinic.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinic.Core.ViewModels.AppointmentVMs
{
	public class TodayAppointmentVM
	{
		public int Id { get; set; }

		public VisitType Type { get; set; }

		public DateTime Date { get; set; }

		public DateTime TimeFrom { get; set; }

		public DateTime TimeTo { get; set; }

		public Patient Patient { get; set; }

		public Doctor Doctor { get; set; }

		public Visit Visit { get; set; }
	}
}
