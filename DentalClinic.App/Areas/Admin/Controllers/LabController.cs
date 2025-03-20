using AutoMapper;
using DentalClinic.Core.Consts;
using DentalClinic.Core.Models;
using DentalClinic.Core.Repositories;
using DentalClinic.Core.ViewModels.DoctorVMs;
using DentalClinic.Core.ViewModels.LabVMs;
using DentalClinic.EF.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.App.Areas.Admin.Controllers
{
	public class LabController : AdminBaseController
	{
		private readonly ILabRepository _labRepository;
		private readonly IPatientRepository _patietRepository;
		private readonly IFinanceRepository _financeRepository;
		private readonly IMapper _mapper;	

        public LabController(ILabRepository labRepository, IPatientRepository patientRepository, IFinanceRepository financeRepository, IMapper mapper)
        {
            _labRepository = labRepository;
			_patietRepository = patientRepository;	
			_financeRepository = financeRepository;
			_mapper = mapper;
        }


		[HttpPost]
		public IActionResult AllData()
		{
			return Ok(_labRepository.DataTableAlldata(Request));
		}


        [HttpGet]
        public IActionResult GetPatients(string searchTerm)
        {
            return Json(_labRepository.searchPatient(searchTerm));
        }

		[HttpGet]
        public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Add(AddLabVM addLabVM)
		{
			if(ModelState.IsValid)
			{
                var patientExisits = _patietRepository.GetById(addLabVM.PatientId);
                if (patientExisits == null || patientExisits.IsDeleted == 1)
                    return NotFound();

                var newLab = _mapper.Map<Lab>(addLabVM);
                _labRepository.Add(newLab);
                return Ok();
            }
            return PartialView("/Areas/Admin/Views/Lab/Add.cshtml", addLabVM);
        }

		[HttpGet]
		public IActionResult Update(int id)
		{
            var labExists = _labRepository.Find(x => x.Id == id, ["Patient"]);
            if (labExists == null || labExists.IsDeleted == 1)
                return NotFound();
			var updateLabVM = _mapper.Map<UpdateLabVM>(labExists);
			updateLabVM.PName = labExists.Patient.Name;
            return PartialView("/Areas/Admin/Views/Lab/Update.cshtml", updateLabVM);
        }

		[HttpPost]
		public IActionResult Update(UpdateLabVM updateLabVM)
		{
			if (ModelState.IsValid)
			{
                var labExists = _labRepository.Find(x=>x.Id == updateLabVM.Id, ["Finance"]);
                if (labExists == null || labExists.IsDeleted == 1)
                    return NotFound();

                var patientExisits = _patietRepository.GetById(updateLabVM.PatientId);
                if (patientExisits == null || patientExisits.IsDeleted == 1)
                    return NotFound();

                if (labExists.ActualReceivingDate == null && updateLabVM.ActualReceivingDate != null /*|| labExists.ActualReceivingDate.Value.Date.Year != '1' && labExists.ActualReceivingDate.Value.Date.Month != '1' && labExists.ActualReceivingDate.Value.Date.Day != '1'*/)
                {
                    var newFinance = new Finance();
                    newFinance.Category = labExists.SampleName;
                    newFinance.Price = labExists.Price;
                    newFinance.FinanceType = FinanceType.Purchase;
                    _financeRepository.Add(newFinance);

					labExists.Finance = newFinance;
                }

				if (labExists.Price != updateLabVM.Price && labExists.Finance != null)
				{
					labExists.Finance.Price = updateLabVM.Price;
					_financeRepository.Update(labExists.Finance);
				}
				
                _mapper.Map(updateLabVM, labExists);
				_labRepository.Update(labExists);

				return Ok();
            }
            return PartialView("/Areas/Admin/Views/Lab/Update.cshtml", updateLabVM);
        }

        [HttpGet]
		public IActionResult Details(int id)
		{
            var labExists = _labRepository.Find(x=>x.Id == id, ["Patient"]);
            if (labExists == null || labExists.IsDeleted == 1)
                return NotFound();

			var detailsLabVM = _mapper.Map<DetailsLabVM>(labExists);
			detailsLabVM.PatientName = labExists.Patient.Name;
            return PartialView("/Areas/Admin/Views/Lab/Details.cshtml", detailsLabVM);
        }

		[HttpPost]
		public IActionResult Delete (int id)
		{
			var labExists = _labRepository.Find(x=>x.Id == id, ["Finance"]);
			if (labExists == null || labExists.IsDeleted == 1)
				return NotFound();


			labExists.IsDeleted = 1;
			_labRepository.Update(labExists);

			if(labExists.Finance != null)
			{
				labExists.Finance.IsDeleted = 1;
				_financeRepository.Update(labExists.Finance);
			}

			return Ok();
		}
	}
}
