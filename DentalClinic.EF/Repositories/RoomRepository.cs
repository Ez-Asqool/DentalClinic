using DentalClinic.Core.Models;
using DentalClinic.Core.Repositories;
using DentalClinic.EF.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinic.EF.Repositories
{
	public class RoomRepository : BaseRepository<Room>, IRoomRepository
	{
		public RoomRepository(ApplicationDbContext context) : base(context)
		{
		}
	}
}
