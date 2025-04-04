﻿using DentalClinic.Core.Models;
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
	public class RoomRepository : BaseRepository<Room>, IRoomRepository
	{
		public RoomRepository(ApplicationDbContext context) : base(context)
		{
		}

		public object DataTableAlldata(HttpRequest Request)
		{
			var pageSize = int.Parse(Request.Form["length"]);
			var skip = int.Parse(Request.Form["start"]);

			string searchValue = Request.Form["search[value]"];

			var sortColumn = Request.Form[string.Concat("columns[", Request.Form["order[0][column]"], "][name]")];
			var sortColumnDirection = Request.Form["order[0][dir]"];

			IQueryable<Room> room = _context.Rooms.Where(x => x.IsDeleted == 0).AsQueryable();
			if (!string.IsNullOrEmpty(searchValue))
			{
				room = room.Where(x =>
				string.IsNullOrEmpty(searchValue) ? true :
				(x.Name.Contains(searchValue)) ||
				(x.Number.ToString().Contains(searchValue)));
			}



			if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
			{
				room = room.OrderBy(string.Concat(sortColumn, " ", sortColumnDirection));
			}

			var data = room.Skip(skip).Take(pageSize).ToList();


			var recordsTotal = room.Count();

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
