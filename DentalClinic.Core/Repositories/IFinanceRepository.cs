using DentalClinic.Core.Consts;
using DentalClinic.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinic.Core.Repositories
{
    public interface IFinanceRepository : IBaseRepository<Finance>
    {
		public object DataTableAlldata(HttpRequest Request, FinanceType financeType);
	}
}
