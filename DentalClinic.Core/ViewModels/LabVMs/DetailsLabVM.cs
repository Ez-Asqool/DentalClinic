using DentalClinic.Core.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinic.Core.ViewModels.LabVMs
{
	public class DetailsLabVM
	{
        public string LabName { get; set; }


        public string SampleName { get; set; }


        public int Quantity { get; set; }


        public SampleType SampleType { get; set; }


        public string Notice { get; set; }


        public DateTime SendingDate { get; set; }


        public DateTime? ReceivingDate { get; set; }


        public DateTime ActualReceivingDate { get; set; }


        public double Price { get; set; }


        public string PatientName { get; set; }
    }
}
