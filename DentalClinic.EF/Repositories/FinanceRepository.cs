using DentalClinic.Core.Consts;
using DentalClinic.Core.Models;
using DentalClinic.Core.Repositories;
using DentalClinic.EF.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinic.EF.Repositories
{
    public class FinanceRepository : BaseRepository<Finance>, IFinanceRepository
    {
        public FinanceRepository(ApplicationDbContext context) : base(context)
        {
        }

		public object DataTableAlldata(HttpRequest Request, FinanceType financeType)
		{
			var pageSize = int.Parse(Request.Form["length"]);
			var skip = int.Parse(Request.Form["start"]);

			string searchValue = Request.Form["search[value]"];

			var sortColumn = Request.Form[string.Concat("columns[", Request.Form["order[0][column]"], "][name]")];
			var sortColumnDirection = Request.Form["order[0][dir]"];

			IQueryable<Finance> finance = _context.Finances.Where(x => x.IsDeleted == 0 && x.FinanceType == financeType).AsQueryable();
			if (!string.IsNullOrEmpty(searchValue))
			{
				finance = finance.Where(x =>
				string.IsNullOrEmpty(searchValue) ? true :
				(x.Category.Contains(searchValue)) ||
				(x.CreatedAt.ToString().Contains(searchValue)) ||
				(x.Price.ToString().Contains(searchValue)));
			}



			if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
			{
				finance = finance.OrderBy(string.Concat(sortColumn, " ", sortColumnDirection));
			}

			var data = finance.Skip(skip).Take(pageSize).ToList();

			
			var recordsTotal = finance.Count();

			var jsonData = new
			{
				recordsFiltered = recordsTotal,
				recordsTotal,
				data = data.Select(a => new
				{
					Id = a.Id,
					Category = a.Category,
					FinanceType = a.FinanceType,
					Price = a.Price,
					CreatedAt = a.CreatedAt.ToString("yyyy-MM-dd")
				})
			};
			return jsonData;
		}
	}
}
