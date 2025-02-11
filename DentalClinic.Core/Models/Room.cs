using DentalClinic.Core.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinic.Core.Models
{
    public class Room : BaseModel
    {
        public int Id { get; set; }


        [Required(ErrorMessage = Messages.ErrorMessage), MaxLength(5)]
        public int Number { get; set; }


        [Required(ErrorMessage = Messages.ErrorMessage), MaxLength(50)]
        public string? Name { get; set; }


        [Required(ErrorMessage = Messages.ErrorMessage), MaxLength(1000)]
        public string? Description { get; set; }


        public List<Doctor> Doctor { get; set; }

        public int ClinicId { get; set; }

        public Clinic Clinic { get; set; }

    }
}
