using DentalClinic.Core.Models;
using DentalClinic.Core.ViewModels.TreatmentVMs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinic.Core.ViewModels.VisitVMs
{
    public class AddVisitVM
    {
        public List<IFormFile> Images { get; set; } 

        public AddTreatmentVM addTreatmentVM { get; set; }
    }
}
