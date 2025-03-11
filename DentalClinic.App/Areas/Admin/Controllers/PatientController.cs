using AutoMapper;
using DentalClinic.Core.Models;
using DentalClinic.Core.Repositories;
using DentalClinic.Core.ViewModels.DoctorVMs;
using DentalClinic.Core.ViewModels.PatientVMs;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.App.Areas.Admin.Controllers
{
    public class PatientController : AdminBaseController
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;   

        public PatientController(IPatientRepository patientRepository, IMapper mapper)
        {
            _patientRepository = patientRepository; 
            _mapper = mapper;   
        }

        [HttpPost]
        public IActionResult AllData()
        {
            return Ok(_patientRepository.DataTableAlldata(Request));
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddPatientVM addPatientVM)
        {
            if (ModelState.IsValid)
			{
                var newPatient = _mapper.Map<Patient>(addPatientVM);
                newPatient.Age = (DateTime.Now.Year - newPatient.DateOfBirth.Year);
                _patientRepository.Add(newPatient);
                return Ok();
            }
			return PartialView("/Areas/Admin/Views/Patient/Add.cshtml", addPatientVM);
		}

        [HttpGet]
        public IActionResult Update(int id)
        {
            var patientExists = _patientRepository.GetById(id);
            if (patientExists == null || patientExists.IsDeleted == 1)
                return NotFound();
            var updatePatientVM = _mapper.Map<UpdatePatientVM>(patientExists);
            return PartialView("/Areas/Admin/Views/Patient/Update.cshtml", updatePatientVM);
        }

        [HttpPost]
        public IActionResult Update(UpdatePatientVM updatePatientVM)
        {
            if (ModelState.IsValid)
            {
                var patientExists = _patientRepository.GetById(updatePatientVM.Id);
                if (patientExists == null || patientExists.IsDeleted == 1)
                    return NotFound();

                if (updatePatientVM.DateOfBirth != patientExists.DateOfBirth)
                    patientExists.Age = (DateTime.Now.Year - updatePatientVM.DateOfBirth.Year);

                _mapper.Map(updatePatientVM, patientExists);
                _patientRepository.Update(patientExists);
                return Ok();
            }
            return PartialView("/Areas/Admin/Views/Patient/Update.cshtml", updatePatientVM);
        }

        //[HttpGet]
        //public IActionResult Details(int id)
        //{
        //    var patientExists = _patientRepository.GetById(id);
        //    if (patientExists == null || patientExists.IsDeleted == 1)
        //        return NotFound();

        //    var detailsPatientVM = _mapper.Map<DetailsPatientVM>(patientExists);
        //    return View(detailsPatientVM);
        //}

        [HttpGet]
        public IActionResult Details(int id)
        {
            var patientExists = _patientRepository.FindWithThenFind(x=>x.Id == id, ["Appointments"], ["Visit"]);
            if (patientExists == null || patientExists.IsDeleted == 1)
                return NotFound();

            var detailsPatientVM = _mapper.Map<DetailsPatientVM>(patientExists);
            detailsPatientVM.allVisitsIds = new List<int>();
            foreach (var appointment in patientExists.Appointments)
            {
                if (appointment.Visit != null)
                {
                    detailsPatientVM.allVisitsIds.Add(appointment.Visit.Id);
                }
            }
            detailsPatientVM.allVisitsIds.Sort();
            return PartialView("/Areas/Admin/Views/Patient/Details.cshtml", detailsPatientVM);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var patientExists = _patientRepository.GetById(id);
            if (patientExists == null || patientExists.IsDeleted == 1)
                return NotFound();

            patientExists.IsDeleted = 1;
            _patientRepository.Update(patientExists);   
            return Ok();    
        }


    }
}
