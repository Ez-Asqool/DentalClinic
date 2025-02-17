using AutoMapper;
using DentalClinic.app.AppServices.FileUploadService;
using DentalClinic.Core.Repositories;
using DentalClinic.Core.ViewModels.ClinicVMs;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.App.Areas.Admin.Controllers
{
    public class ClinicController : AdminBaseController
    {
        protected readonly IClinicRepository _clinicRepository;
        protected readonly IMapper _mapper;
        private readonly IImageService _imageService;
        public ClinicController(IClinicRepository clinicRepository, IMapper mapper, IImageService imageService)
        {
            _clinicRepository = clinicRepository;
            _mapper = mapper;
            _imageService = imageService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var clinic = _clinicRepository.Find(x => x.IsDeleted == 0);
            var indexVM = _mapper.Map<IndexVM>(clinic);
            return View(indexVM);
        }

        [HttpPost]
        public IActionResult Update(IndexVM indexVM)
        {
            if (ModelState.IsValid)
            {
				var clinic = _clinicRepository.Find(x => x.IsDeleted == 0);
				_mapper.Map(indexVM, clinic);
				if (indexVM.Logo != null)
					clinic.LogoName = _imageService.updateImage("AppImages/ClinicImages", indexVM.Logo, clinic.LogoName);
				_clinicRepository.Update(clinic);
				return Ok();
			}
			return PartialView("/Areas/Admin/Views/Clinic/Add.cshtml", indexVM);
		}
    }
}
