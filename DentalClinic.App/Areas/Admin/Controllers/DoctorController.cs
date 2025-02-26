using AutoMapper;
using DentalClinic.app.AppServices.FileUploadService;
using DentalClinic.Core.Models;
using DentalClinic.Core.Repositories;
using DentalClinic.Core.ViewModels.DoctorVMs;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.App.Areas.Admin.Controllers
{
	public class DoctorController : AdminBaseController
	{
		private readonly IDoctorRepository _doctorRepository;
		private readonly IRoomRepository _roomRepository;
		private readonly IMapper _mapper;
		private readonly IImageService _imageService;
        public DoctorController(IDoctorRepository doctorRepository,IRoomRepository roomRepository, IMapper mapper, IImageService imageService)
        {
			_doctorRepository = doctorRepository;
			_roomRepository = roomRepository;	
			_mapper = mapper;	
			_imageService = imageService;	
        }

        [HttpPost]
        public IActionResult AllData()
        {
        	return Ok(_doctorRepository.DataTableAlldata(Request));
        }

        [HttpGet]
		public IActionResult Index()
		{
			var rooms = _roomRepository.FindAll(x=> x.IsDeleted == 0);
			var addDoctorVM = new AddDoctorVM();
			addDoctorVM.Rooms = _mapper.Map<List<IndexVM>>(rooms);

			return View(addDoctorVM);
		}

		[HttpPost]
		public IActionResult Add(AddDoctorVM addDoctorVM)
		{
			if (ModelState.IsValid)
			{
				var newDoctor = _mapper.Map<Doctor>(addDoctorVM);
				if(addDoctorVM.Image != null)
				{
					var imageName = _imageService.uploadImage("AppImages/DoctorImages", addDoctorVM.Image);
					newDoctor.ImageName = imageName;
				}
				else
				{
					newDoctor.ImageName = "blank.png";
				}
				var Age = (DateTime.Now.Year - addDoctorVM.DateOfBirth.Year);
				newDoctor.Age = Age;
				_doctorRepository.Add(newDoctor);
				return Ok();
			}
			return PartialView("/Areas/Admin/Views/Doctor/Add.cshtml", addDoctorVM);
		}

		[HttpGet]
		public IActionResult Update(int id) 
		{
			var doctorExists = _doctorRepository.GetById(id);
			if (doctorExists == null || doctorExists.IsDeleted == 1)
				return NotFound();


			var updateDoctorVM = _mapper.Map<UpdateDoctorVM>(doctorExists);
            var rooms = _roomRepository.FindAll(x=> x.IsDeleted == 0);
			updateDoctorVM.Rooms = _mapper.Map<List<IndexVM>>(rooms);
            return PartialView("/Areas/Admin/Views/Doctor/Update.cshtml", updateDoctorVM);
        }

		[HttpPost]
		public IActionResult Update(UpdateDoctorVM updateDoctorVM)
		{
			if (ModelState.IsValid)
			{
				var doctorExists = _doctorRepository.GetById(updateDoctorVM.Id);
				if (doctorExists == null || doctorExists.IsDeleted == 1)
					return NotFound();
				
				if(updateDoctorVM.DateOfBirth != doctorExists.DateOfBirth)
					doctorExists.Age = (DateTime.Now.Year - updateDoctorVM.DateOfBirth.Year);

				_mapper.Map(updateDoctorVM, doctorExists);

				if (updateDoctorVM.Image != null)
					doctorExists.ImageName = _imageService.updateImage("AppImages/DoctorImages", updateDoctorVM.Image, doctorExists.ImageName);

				_doctorRepository.Update(doctorExists);
				return Ok();
			}

			return PartialView("/Areas/Admin/Views/Doctor/Update.cshtml", updateDoctorVM);
		}

		[HttpGet]
		public IActionResult Details(int id) 
		{
			var doctorExists = _doctorRepository.Find(x => x.Id == id, ["Room"]);
			if (doctorExists == null || doctorExists.IsDeleted == 1)
				return NotFound();
			var detailsDoctorVM = _mapper.Map<DetailsDoctorVM>(doctorExists);
			return PartialView("/Areas/Admin/Views/Doctor/Details.cshtml", detailsDoctorVM);
		}

		[HttpPost]
		public IActionResult Delete(int id) 
		{
			var doctorExists = _doctorRepository.GetById(id);
			if (doctorExists == null || doctorExists.IsDeleted == 1)
				return NotFound();
			doctorExists.IsDeleted = 1;
			_doctorRepository.Update(doctorExists);
			return Ok();
		}

	}
}
