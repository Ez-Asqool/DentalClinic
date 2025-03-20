using AutoMapper;
using DentalClinic.Core.Models;
using DentalClinic.Core.Repositories;
using DentalClinic.Core.ViewModels.AppointmentVMs;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.App.Areas.Admin.Controllers
{
    public class AppointmentController : AdminBaseController
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;

        public AppointmentController(IAppointmentRepository appointmentRepository, IDoctorRepository doctorRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository; 
            _doctorRepository = doctorRepository; 
            _mapper = mapper;
        }


		[HttpPost]
		public IActionResult AllData()
		{
			return Ok(_appointmentRepository.DataTableAlldata(Request));
		}


		[HttpGet]
        public IActionResult GetPatients(string searchTerm)
        {
            return Json(_appointmentRepository.searchPatient(searchTerm));
        }

        

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            var doctors = _doctorRepository.FindAll(d=> d.IsDeleted == 0);
            var addAppointmentVM = new AddAppointmentVM();
            addAppointmentVM.Doctors = doctors;
            return View(addAppointmentVM);
        }

        
        [HttpPost]
        public IActionResult Add(AddAppointmentVM addAppointmentVM)
        {
            if(ModelState.IsValid)
            {
                if (addAppointmentVM.PatientId == 0)
                        return StatusCode(401);

                var appointments = _appointmentRepository.FindAll(a => a.Date == addAppointmentVM.Date && a.DoctorId == addAppointmentVM.DoctorId);
                foreach (var appointment in appointments)
                    if (appointment.TimeFrom == addAppointmentVM.TimeFrom)
                        return StatusCode(400);

                var newAppointment = _mapper.Map<Appointment>(addAppointmentVM);
                newAppointment.TimeTo = newAppointment.TimeFrom.AddMinutes(30);
                _appointmentRepository.Add(newAppointment); 
                return StatusCode(200);
            }
            return BadRequest();    
        }

		[HttpPost]
		public IActionResult Check(AddAppointmentVM addAppointmentVM)
		{
			var appointments = _appointmentRepository.FindAll(a => a.Date == addAppointmentVM.Date && a.DoctorId == addAppointmentVM.DoctorId);

			foreach (var appointment in appointments)
				if (appointment.TimeFrom == addAppointmentVM.TimeFrom)
					return StatusCode(400);

			return StatusCode(200);
		}

        [HttpGet]
        public IActionResult Today()
        {
            var todayAppointments = _appointmentRepository.FindAll(x => x.Date.Date == DateTime.Now.Date && x.IsDeleted ==0, ["Doctor", "Patient", "Visit"]);
			List<TodayAppointmentVM> todayAppointmentsVm = _mapper.Map<List<TodayAppointmentVM>>(todayAppointments);
            return View(todayAppointmentsVm);
        }



	}
}
