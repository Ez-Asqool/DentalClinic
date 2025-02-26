using DentalClinic.Core.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinic.Core.ViewModels.PatientVMs
{
	public class DetailsPatientVM
	{
		public int Id { get; set; }


		public string Name { get; set; }


		public int Age { get; set; }


		public DateTime DateOfBirth { get; set; }


		public Gender Gender { get; set; }


		public string? Job { get; set; }


		public string Address { get; set; }


		public string Firstphone { get; set; }


		public string? Secondphone { get; set; }


		public string? PersonalIDNumber { get; set; }


		public string? InsuranceCardNumber { get; set; }


		public string? Notes { get; set; }
	}
}
