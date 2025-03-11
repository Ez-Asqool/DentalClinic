using AutoMapper;
using DentalClinic.app.AppServices.FileUploadService;
using DentalClinic.Core.Models;
using DentalClinic.Core.Repositories;
using DentalClinic.Core.ViewModels.TreatmentVMs;
using DentalClinic.Core.ViewModels.VisitVMs;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace DentalClinic.App.Areas.Admin.Controllers
{
    public class VisitController : AdminBaseController
    {
        private readonly IAppointmentRepository _appointmentRepository; 
        private readonly IVisitRepository _visitRepository;
        private readonly IImageRepository _imageRepository;
        private readonly ITreatmentRepository _trereatmentRepository;
        private readonly IPatientRepository _patientRepository; 
        private readonly IMapper _mapper;   
        private readonly IImageService _imageService;

        public VisitController(IAppointmentRepository appointmentRepository, IVisitRepository visitRepository, IImageRepository imageRepository, ITreatmentRepository treatmentRepository, IPatientRepository patientRepository, IMapper mapper, IImageService imageService)
        {
            _appointmentRepository = appointmentRepository;
            _visitRepository = visitRepository;
            _imageRepository = imageRepository; 
            _trereatmentRepository = treatmentRepository;  
            _patientRepository = patientRepository; 
            _mapper = mapper;   
            _imageService = imageService;   
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        [HttpGet]
        public IActionResult Add(int id)
        {
			//Check Appointment Existance
			var appointmentExists = _appointmentRepository.Find(x=> x.Id == id, ["Patient", "Doctor"]);
			if (appointmentExists == null || appointmentExists.IsDeleted == 1)
				return BadRequest();


			var addTreatmentVm = new AddTreatmentVM();
            addTreatmentVm.AppointmentId = id;
            addTreatmentVm.Appointment = appointmentExists;

            return View(addTreatmentVm);
        }

        [HttpPost]
        public IActionResult Add(List<AddTreatmentVM> addTreatmentVMs)
        {
            if (ModelState.IsValid) 
            {
                //Check Appointment Existance
                var appointmentExists = _appointmentRepository.GetById(addTreatmentVMs.First().AppointmentId);
                if (appointmentExists == null || appointmentExists.IsDeleted == 1)
                    return BadRequest();

                //Add New Visit
                var newVisit = new Visit();
                newVisit.Appointment = appointmentExists;
                newVisit.Date = DateTime.Now;
                newVisit.Type = appointmentExists.Type;

                _visitRepository.Add(newVisit);

                //Add Treatments 
                foreach (var addTreatmentVM in addTreatmentVMs)
                {
                    var newTreatment = new Treatment();
                    newTreatment.Visit = newVisit;
                    newTreatment.PlaceOfTreatment = addTreatmentVM.PlaceOfTreatment;
                    newTreatment.TypeOfTreatment = addTreatmentVM.TypeOfTreatment;
                    newTreatment.Notice = addTreatmentVM.Notice;
                    newTreatment.Price = addTreatmentVM.Price;
                    newTreatment.Discount = addTreatmentVM.Discount;
                    newTreatment.Total = addTreatmentVM.Total;  

                    _trereatmentRepository.Add(newTreatment);

                    //Add Images For Treatment
                    if(addTreatmentVM.Images != null)
                    {
                        foreach (var image in addTreatmentVM.Images)
                        {
                            var newImage = new Image();
                            newImage.Name = _imageService.uploadImage("AppImages/TreatmentImages", image);
                            newImage.Treatment = newTreatment;
                            _imageRepository.Add(newImage);
                        }
                    }
                }

                return Json("Visit Done");

            }
            return BadRequest();
        }



        public IActionResult Details(int id)
        {
            var visitExists = _visitRepository.FindWithThenFind(x=> x.Id == id, ["Treatments", "Appointment", "Appointment"], ["Images", "Patient", "Doctor"]);
            var detailsVisitVM = _mapper.Map<DetailsVisitVM>(visitExists);
            detailsVisitVM.PatientName = visitExists.Appointment.Patient.Name;
            detailsVisitVM.DoctorName = visitExists.Appointment.Doctor.Name;
            detailsVisitVM.AppointmentDate = visitExists.Appointment.CreatedAt;
            detailsVisitVM.AppointmentId = visitExists.Appointment.Id;

            detailsVisitVM.allVisitsIds = new List<int>();
            var patientExists = _patientRepository.FindWithThenFind(x=> x.Id == visitExists.Appointment.Patient.Id, ["Appointments"], ["Visit"]);
            foreach (var appointment in patientExists.Appointments)
            {
                if (appointment.IsDeleted == 0 && appointment.Visit != null)
                {
                    detailsVisitVM.allVisitsIds.Add(appointment.Visit.Id);
                }
            }
            detailsVisitVM.allVisitsIds.Sort();
            return View(detailsVisitVM);

        }
    }
}
