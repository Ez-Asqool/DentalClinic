using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinic.Core.Models
{
    public class Disease : BaseModel
    {
        public int Id { get; set; }

        public int Name { get; set; }

        public string? Description { get; set; }

        public DateTime StartingDate { get; set; }

        //patient diseases
    }
}
