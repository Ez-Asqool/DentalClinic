using DentalClinic.Core.Consts;
using DentalClinic.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinic.Core.ViewModels.TreatmentVMs
{
    public class DetailsTreatmentVM
    {

        public string PlaceOfTreatment { get; set; }


        public string TypeOfTreatment { get; set; }


        public string? Notice { get; set; }


        public double Price { get; set; }


        public double? Discount { get; set; }


        public double Total { get; set; }


        public List<Image> Images { get; set; }


        public Visit Visit { get; set; }
    }
}
