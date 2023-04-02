using System;
using System.ComponentModel.DataAnnotations;

namespace ordermanager_dotnet.Models
{
    public class AgriculturalInput
    {
        [Required]
        public int Id {get;set;}

        [Required]
        public string Name {get;set;}

        [Required]
        public string Sprayrate {get;set;}

        [Required]
        public float Price {get;set;}

        [Required]
        public int Quantity {get;set;}

        [Required]
        public DateTime AcquisitionDate { get; set; }

        [Required]
        public int ProviderId {get;set;}
        public Provider Providers {get;set;}
    }
}
