﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinic.Core.Models
{
    public class Image
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TreatmentId { get; set; }
        public Treatment Treatment { get; set; }
    }
}
