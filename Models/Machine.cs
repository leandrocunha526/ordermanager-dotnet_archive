using System;
using System.ComponentModel.DataAnnotations;

namespace ordermanager_dotnet.Models
{
    public class Machine
    {
        [Required]
        public int Id { get; set; }

        //Id agro register
        [Required]
        public string RegisterCode {get;set;}

        //Serial number of machine
        [Required]
        public string SerialNumber {get;set;}

        [Required]
        public string Type { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public string Implement { get; set; }

        [Required]
        public DateTime AcquisitionDate { get; set; }

        [Required]
        public int ModelMachineId { get; set; }

        public ModelMachine ModelsMachine { get; set; }
    }
}
