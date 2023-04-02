using System;
using System.ComponentModel.DataAnnotations;

namespace ordermanager_dotnet.Models
{
    public class Order
    {
        [Required]
        public int Id {get;set;}

        [Required]
        public string Description {get;set;}

        [Required]
        public string Local {get;set;}

        [Required]
        public DateTime StartDate {get;set;}

        [Required]
        public string Status {get;set;}

        [Required]
        public float Price {get;set;}

        [Required]
        public DateTime EndDate {get;set;}

        [Required]
        public int MachineId {get;set;}

        public Machine Machines {get;set;}

        [Required]
        public int EmployeeId {get;set;}

        public Employee Employees {get;set;}

        public int AgriculturalInputId {get;set;}

        public AgriculturalInput AgriculturalInputs {get;set;}
    }
}
