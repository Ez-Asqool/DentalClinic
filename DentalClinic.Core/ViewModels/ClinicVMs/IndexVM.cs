using DentalClinic.Core.Consts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinic.Core.ViewModels.ClinicVMs
{
	public class IndexVM
	{

		[Required(ErrorMessage = Messages.ErrorMessage), MaxLength(120)]
		public string Name { get; set; }


		[Required(ErrorMessage = Messages.ErrorMessage), MaxLength(20)]
		public string Phone { get; set; }


		[Required(ErrorMessage = Messages.ErrorMessage), MaxLength(150)]
		public string Address { get; set; }


		[MaxLength(150)]
		public string? LogoName { get; set; }
        public IFormFile? Logo { get; set; }


    }
}
