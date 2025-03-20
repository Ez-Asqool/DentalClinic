using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinic.Core.Consts
{
    public class Enum
    {
    }

    public enum VisitType 
    {
        New,
        Review
    }

    public enum Gender
    {
        Male,
        Female
    }

    public enum FinanceType
    {
        Purchase,   //مشتريات
        Income,     //مدخولات
        Expense,    //مصروفات
    }

    public enum SampleType
    {
        New,
		Repair
	}

}
