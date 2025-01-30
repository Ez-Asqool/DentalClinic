using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinic.Core.Models
{
    public class File : BaseModel
    {
        public int Id { get; set; }

        public string TreatmentPlan { get; set; }

        public List<string> ImagesNames { get; set; }

    }
}
