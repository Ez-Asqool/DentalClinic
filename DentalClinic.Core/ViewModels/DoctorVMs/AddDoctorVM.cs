using DentalClinic.Core.Consts;
using DentalClinic.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinic.Core.ViewModels.DoctorVMs
{
	public class AddDoctorVM
	{

		[Required(ErrorMessage = Messages.ErrorMessage), MaxLength(50)]
		public string Name { get; set; }


		[Required(ErrorMessage = Messages.ErrorMessage), MaxLength(50)]
		public string JobNumber { get; set; }


		[Required(ErrorMessage = Messages.ErrorMessage)]
		public DateTime DateOfBirth { get; set; }


		[Required(ErrorMessage = Messages.ErrorMessage), Range(0, 70)]
		public int ExperienceYears { get; set; }


		[Required(ErrorMessage = Messages.ErrorMessage)]
		public int GraduationYear { get; set; }


		[Required(ErrorMessage = Messages.ErrorMessage), MaxLength(20)]
		public string Phone { get; set; }


		[Required(ErrorMessage = Messages.ErrorMessage), MaxLength(50)]
		public string Email { get; set; }

		public IFormFile? Image { get; set; }


		[Required(ErrorMessage = Messages.ErrorMessage)]
		public Gender Gender { get; set; }

		public int RoomId { get; set; }

		public List<IndexVM>? Rooms { get; set; }
	}
}
