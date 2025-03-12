using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinic.Core.ViewModels.FinanceVMs
{
	public class IndexVM
	{
        public DateTime date { get; set; }
        //For Day
        public double dayIncomes { get; set; }
        public double dayPurchases { get; set; }
        public double dayExpences { get; set; }
        public double daySammary { get; set; }

        //For Week
        public double weekIncomes { get; set; }
        public double weekPurchases { get; set; }
        public double weekExpences { get; set; }
        public double weekSammary { get; set; }

        //For Month
        public double monthIncomes { get; set; }
        public double monthPurchases { get; set; }
        public double monthExpences { get; set; }
        public double monthSammary { get; set; }

        //For Year
        public double yearIncomes { get; set; }
        public double yearPurchases { get; set; }
        public double yearExpences { get; set; }
        public double yearSammary { get; set; }



    }
}
