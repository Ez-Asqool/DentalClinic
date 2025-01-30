using DentalClinic.Core.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinic.Core.Models
{
    public class Treatment : BaseModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = Messages.ErrorMessage), MaxLength(1)]
        public Char PlaceOfTreatment { get; set; }

        [Required(ErrorMessage = Messages.ErrorMessage), MaxLength(50)]
        public string TypeOfTreatment { get; set; }

        [Required(ErrorMessage = Messages.ErrorMessage), MaxLength(1000)]
        public string? Notice { get; set; }

        [Required(ErrorMessage = Messages.ErrorMessage), MaxLength(5)]
        public double Price { get; set; }


        [Required(ErrorMessage = Messages.ErrorMessage), MaxLength(5)]
        public double Discount { get; set; }


        [Required(ErrorMessage = Messages.ErrorMessage), MaxLength(5)]
        public double Total { get; set; }

        public int VisitId { get; set; }
        public Visit Visit { get; set; }


    }
}
