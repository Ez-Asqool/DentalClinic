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
    }
}
