using System.ComponentModel.DataAnnotations;
using System;

namespace ordermanager_dotnet.Models
{
    public class Employee
    {
        [Required]
        public int Id {get;set;}

        [Required]
        public string Name {get;set;}

        [Required]
        public float Salary {get;set;}

        [Required]
        public string Cpf {get;set;}

        [Required]
        public string Phone {get;set;}

        [Required]
        public DateTime Birthday {get;set;}
    }
}
