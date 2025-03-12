using DentalClinic.Core.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinic.Core.ViewModels.FinanceVMs
{
    public class UpdateFinanceVM
    {
        public int Id { get; set; }


        [Required(ErrorMessage = Messages.ErrorMessage), MaxLength(150)]
        public string Category { get; set; }


        //[Required(ErrorMessage = Messages.ErrorMessage), MaxLength(9)]
        public FinanceType FinanceType { get; set; }


        [Required(ErrorMessage = Messages.ErrorMessage)]
        [Range(0, 99999)]
        public double Price { get; set; }
    }
}
