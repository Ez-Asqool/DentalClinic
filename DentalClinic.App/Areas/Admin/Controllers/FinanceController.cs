using AutoMapper;
using DentalClinic.Core.Consts;
using DentalClinic.Core.Models;
using DentalClinic.Core.Repositories;
using DentalClinic.Core.ViewModels.FinanceVMs;
using DentalClinic.Core.ViewModels.PatientVMs;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.App.Areas.Admin.Controllers
{
    public class FinanceController : AdminBaseController
    {
        private readonly IFinanceRepository _financeRepository;
        private readonly ITreatmentRepository _treatmentRepository;
        private readonly IMapper _mapper;

        public FinanceController(IFinanceRepository financeRepository, ITreatmentRepository treatmentRepository, IMapper mapper)
        {
            _financeRepository = financeRepository;
			_treatmentRepository = treatmentRepository;
            _mapper = mapper;   
        }


        [HttpPost]
        public IActionResult AllData(string path)
        {
            FinanceType financeType = FinanceType.Purchase;
            
            if (path.Contains("Expense"))
            {
                financeType = FinanceType.Expense;
            }

            return Ok(_financeRepository.DataTableAlldata(Request, financeType));
        }

        [HttpGet]
        public IActionResult Index(DateTime date)
        {
			var indexVM = new IndexVM();

			if (date.Day.ToString() == "1" && date.Month.ToString() == "1" && date.Year.ToString() == "1")
			{
				date = DateTime.Now;
				indexVM.date = date;	
			}

            
            //For Day:
            //Incomes
            var dayIncomes = 0.0;
			try
			{
				var dayTreatments = _treatmentRepository.FindAll(x => x.CreatedAt.Day == date.Day && x.IsDeleted == 0);
				foreach (var treatment in dayTreatments)
				{
					dayIncomes += treatment.Total;
				}
			}
			catch (Exception ex) { }
            indexVM.dayIncomes = dayIncomes;    

            //Purchase
            var dayPurchases = 0.0;
			try
			{
				var dayPurchasesAdded = _financeRepository.FindAll(x => x.FinanceType == FinanceType.Purchase && x.CreatedAt.Day == date.Day && x.IsDeleted == 0);
				foreach (var purchase in dayPurchasesAdded)
				{
					dayPurchases += purchase.Price;
				}
			}
			catch (Exception ex) { }
            indexVM.dayPurchases = dayPurchases;

            //Expense
            var dayExpences = 0.0;
			try
			{
				var dayExpensesAdded = _financeRepository.FindAll(x => x.FinanceType == FinanceType.Expense && x.CreatedAt.Day == date.Day && x.IsDeleted == 0);
				foreach (var expence in dayExpensesAdded)
				{
					dayExpences += expence.Price;
				}
			}
			catch (Exception ex) { }
            indexVM.dayExpences = dayExpences;

			//Sammary
			var daySammary = dayIncomes - (dayPurchases + dayExpences);
            indexVM.daySammary = daySammary;

								/********************************************************/
			//For week:
			// Calculate week number: e.g., if date.Day is 5, weekNumber becomes 1; if 12, then weekNumber becomes 2.
			int weekNumber = ((date.Day - 1) / 7) + 1;
			int startDay = ((weekNumber - 1) * 7) + 1;
			int endDay = weekNumber * 7;

			// Ensure we do not exceed the days in the month.
			int daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
			if (endDay > daysInMonth)
			{
				endDay = daysInMonth;
			}

			// Define start and end of the week. Here, endOfWeek is inclusive.
			DateTime startOfWeek = new DateTime(date.Year, date.Month, startDay);
			DateTime endOfWeek = new DateTime(date.Year, date.Month, endDay);

			//Incomes
			var weekIncomes = 0.0;
			try
			{
				var weekTreatments = _treatmentRepository.FindAll(x => x.CreatedAt.Date >= startOfWeek.Date && x.CreatedAt.Date <= endOfWeek.Date && x.IsDeleted == 0);
				foreach (var treatment in weekTreatments)
				{
					weekIncomes += treatment.Total;
				}
			}
			catch (Exception ex) { }
			indexVM.weekIncomes = weekIncomes;

			//Purchase
			var weekPurchases = 0.0;
			try
			{
				var weekPurchasesAdded = _financeRepository.FindAll(x => x.FinanceType == FinanceType.Purchase && x.CreatedAt.Date >= startOfWeek.Date && x.CreatedAt.Date <= endOfWeek.Date && x.IsDeleted == 0);
				foreach (var purchase in weekPurchasesAdded)
				{
					weekPurchases += purchase.Price;
				}
			}
			catch (Exception ex) { }
			indexVM.weekPurchases = weekPurchases;

			//Expense
			var weekExpences = 0.0;
			try
			{
				var weekExpensesAdded = _financeRepository.FindAll(x => x.FinanceType == FinanceType.Expense && x.CreatedAt.Date >= startOfWeek.Date && x.CreatedAt.Date <= endOfWeek.Date && x.IsDeleted == 0);
				foreach (var expence in weekExpensesAdded)
				{
					weekExpences += expence.Price;
				}
			}
			catch (Exception ex) { }
			indexVM.weekExpences = weekExpences;

			//Sammary
			var weekSammary = weekIncomes - (weekPurchases + weekExpences);
			indexVM.weekSammary = weekSammary;


								/********************************************************/
			//For Month:
			//Incomes
			var monthIncomes = 0.0;
			try
			{
				var monthTreatments = _treatmentRepository.FindAll(x => x.CreatedAt.Month == date.Month && x.IsDeleted == 0);
				foreach (var treatment in monthTreatments)
				{
					monthIncomes += treatment.Total;
				}
			}
			catch (Exception ex) { }
			indexVM.monthIncomes = monthIncomes;

			//Purchase
			var monthPurchases = 0.0;
			try
			{
				var monthPurchasesAdded = _financeRepository.FindAll(x => x.FinanceType == FinanceType.Purchase && x.CreatedAt.Month == date.Month && x.IsDeleted == 0);
				foreach (var purchase in monthPurchasesAdded)
				{
					monthPurchases += purchase.Price;
				}
			}
			catch (Exception ex) { }
			indexVM.monthPurchases = monthPurchases;

			//Expense
			var monthExpences = 0.0;
			try
			{
				var monthExpensesAdded = _financeRepository.FindAll(x => x.FinanceType == FinanceType.Expense && x.CreatedAt.Month == date.Month && x.IsDeleted == 0);
				foreach (var expence in monthExpensesAdded)
				{
					monthExpences += expence.Price;
				}
			}
			catch (Exception ex) { }
			indexVM.monthExpences = monthExpences;

            //Sammary
			var monthSammary = monthIncomes - (monthPurchases + monthExpences);
			indexVM.monthSammary = monthSammary;

								
								/********************************************************/

			//For Year:
			//Incomes
			var yearIncomes = 0.0;
			try
			{
				var yearTreatments = _treatmentRepository.FindAll(x => x.CreatedAt.Year == date.Year && x.IsDeleted == 0);
				foreach (var treatment in yearTreatments)
				{
					yearIncomes += treatment.Total;
				}
			}
			catch (Exception ex){}
			indexVM.yearIncomes = yearIncomes;

			//Purchase
			var yearPurchases = 0.0;
			try
			{
				var yearPurchasesAdded = _financeRepository.FindAll(x => x.FinanceType == FinanceType.Purchase && x.CreatedAt.Year == date.Year && x.IsDeleted == 0);
				foreach (var purchase in yearPurchasesAdded)
				{
					yearPurchases += purchase.Price;
				}
			}
			catch (Exception ex){}
			indexVM.yearPurchases = yearPurchases;

			//Expense
			var yearExpences = 0.0;
			try
			{
				var yearExpensesAdded = _financeRepository.FindAll(x => x.FinanceType == FinanceType.Expense && x.CreatedAt.Month == date.Month && x.IsDeleted == 0);
				foreach (var expence in yearExpensesAdded)
				{
					yearExpences += expence.Price;
				}
			}
			catch (Exception ex){}
			indexVM.yearExpences = yearExpences;

			//Sammary
			var yearSammary = yearIncomes - (yearPurchases + yearExpences);
			indexVM.yearSammary = yearSammary;



			return View(indexVM);
			
		}


		[HttpGet("/Admin/Purchase")]
		public IActionResult Purchase()
        {
            return View();
        }

		[HttpGet("/Admin/Expense")]
		public IActionResult Expense()
		{
			return View();
		}

		public IActionResult Add(AddFinanceVM addFinanceVM)
        {
            if (ModelState.IsValid)
			{
                var newFinance = _mapper.Map<Finance>(addFinanceVM);
                _financeRepository.Add(newFinance);
                return Ok();
			}
            return PartialView("/Areas/Admin/Views/Finance/Add.cshtml", addFinanceVM);
		}


        [HttpGet]
        public IActionResult Update(int id)
        {
            var financeExisits = _financeRepository.GetById(id);
            if (financeExisits == null || financeExisits.IsDeleted == 1)
                return NotFound();

            var updateFinanceVM = _mapper.Map<UpdateFinanceVM>(financeExisits);
            return PartialView("/Areas/Admin/Views/Finance/Update.cshtml", updateFinanceVM);
        }

        [HttpPost]
        public IActionResult Update(UpdateFinanceVM updateFinanceVM)
        {
            if(ModelState.IsValid)
            {
                var financeExists = _financeRepository.GetById(updateFinanceVM.Id);
                if (financeExists == null || financeExists.IsDeleted == 1)
                    return NotFound();

                _mapper.Map(updateFinanceVM, financeExists);

                _financeRepository.Update(financeExists);
                return Ok();
            }
            return PartialView("/Areas/Admin/Views/Finance/Update.cshtml", updateFinanceVM);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var financeExists = _financeRepository.GetById(id);
            if (financeExists == null || financeExists.IsDeleted == 1)
                return NotFound();

            financeExists.IsDeleted = 1;
            _financeRepository.Update(financeExists);
            return Ok();
        }
    }
}
