using DentalClinic.Core.Consts;
using DentalClinic.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinic.Core.ViewModels.DoctorVMs
{
	public class DetailsDoctorVM
	{
		public string Name { get; set; }

		public string JobNumber { get; set; }

		public DateTime DateOfBirth { get; set; }

		public int Age { get; set; }

		public int ExperienceYears { get; set; }

		public int GraduationYear { get; set; }

		public string Phone { get; set; }

		public string Email { get; set; }

		public string? ImageName { get; set; }

		public Gender Gender { get; set; }

		public Room Room { get; set; }
	}
}
