using AutoMapper;
using Azure.Core;
using DentalClinic.Core.Models;
using DentalClinic.Core.Repositories;
using DentalClinic.Core.ViewModels.RoomVMs;
using DentalClinic.EF.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.App.Areas.Admin.Controllers
{
	public class RoomController : AdminBaseController
	{
		private readonly IRoomRepository _roomRepository;
		private readonly IClinicRepository _clinicRepository;
		private readonly IMapper _mapper;

        public RoomController(IRoomRepository roomRepository, IClinicRepository clinicRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
			_clinicRepository = clinicRepository;	
			_mapper = mapper;	
        }

		[HttpPost]
		public IActionResult AllData()
		{
			return Ok(_roomRepository.DataTableAlldata(Request));
		}

        [HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Add(AddRoomVM addRoomVM)
		{
			if (ModelState.IsValid)
			{
				var newRoom = _mapper.Map<Room>(addRoomVM);
				
				var clinic = _clinicRepository.Find(x => x.IsDeleted == 0);
				newRoom.ClinicId = clinic.Id;
				_roomRepository.Add(newRoom);
				
				return Ok();
			}
			return PartialView("/Areas/Admin/Views/Room/Add.cshtml", addRoomVM);
		}

		[HttpGet]
		public IActionResult Update(int id)
		{
            var roomExists = _roomRepository.GetById(id);
            if (roomExists == null || roomExists.IsDeleted == 1)
                return NotFound();

			var updateRoomVM = _mapper.Map<UpdateRoomVM>(roomExists);
            return PartialView("/Areas/Admin/Views/Room/Update.cshtml", updateRoomVM);
        }

		[HttpPost]
		public IActionResult Update(UpdateRoomVM updateRoomVM)
		{
			if(ModelState.IsValid)
			{
                var roomExists = _roomRepository.GetById(updateRoomVM.Id);
                if (roomExists == null || roomExists.IsDeleted == 1)
                    return NotFound();

				_mapper.Map(updateRoomVM, roomExists);
				_roomRepository.Update(roomExists);
				return Ok();
            }
            return PartialView("/Areas/Admin/Views/Room/Update.cshtml", updateRoomVM);
        }

		[HttpGet]
		public IActionResult Details(int id)
		{
			var roomExists = _roomRepository.Find(x => x.Id == id, ["Doctors"]);
			if (roomExists == null || roomExists.IsDeleted == 1)
				return NotFound();

			var detailsRoomVM = _mapper.Map<DetailsRoomVM>(roomExists);
			return PartialView("/Areas/Admin/Views/Room/Details.cshtml", detailsRoomVM);
		}

		[HttpPost]
		public IActionResult Delete(int id)
		{
			var roomExists = _roomRepository.GetById(id);
			if (roomExists == null || roomExists.IsDeleted == 1)
				return NotFound();

			roomExists.IsDeleted = 1;
			_roomRepository.Update(roomExists);
			return Ok();
		}
	}
}
