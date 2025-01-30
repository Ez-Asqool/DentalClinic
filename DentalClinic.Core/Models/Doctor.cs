using DentalClinic.Core.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinic.Core.Models
{
    public class Doctor : BaseModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = Messages.ErrorMessage), MaxLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = Messages.ErrorMessage), MaxLength(50)]
        public string JobNumber { get; set; }

        public DateTime DateOfBirth { get; set; }


        [Required(ErrorMessage = Messages.ErrorMessage), MaxLength(4)]
        public int Age { get; set; }

        [Required(ErrorMessage = Messages.ErrorMessage), MaxLength(2)]
        public int ExperienceYears { get; set; }

        [Required(ErrorMessage = Messages.ErrorMessage), MaxLength(4)]
        public int GraduationYear { get; set; }

        [Required(ErrorMessage = Messages.ErrorMessage), MaxLength(20)]
        public string Phone { get; set; }

        [Required(ErrorMessage = Messages.ErrorMessage), MaxLength(50)]
        public string Email { get; set; }

        [Required(ErrorMessage = Messages.ErrorMessage), MaxLength(150)]
        public string ImageName { get; set; }

        [Required(ErrorMessage = Messages.ErrorMessage), MaxLength(8)]
        public string Gender { get; set; } //enum

        public List<Patient> Patients { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}
