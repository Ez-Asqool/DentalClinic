using DentalClinic.Core.Consts;
using DentalClinic.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinic.Core.ViewModels.RoomVMs
{
	public class DetailsRoomVM
	{
		public int Number { get; set; }

		public string? Name { get; set; }

		public string? Description { get; set; }

		public List<Doctor> Doctors { get; set; }
	}
}
