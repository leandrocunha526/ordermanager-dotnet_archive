using System.ComponentModel.DataAnnotations;

namespace ordermanager_dotnet.Models
{
    public class Provider
    {
        [Required]
        public int Id {get;set;}
        [Required]
        public string CorporateName {get;set;}
        [Required]
        public string Cnpj {get;set;}
        [Required]
        public string Email {get;set;}
        [Required]
        public string Phone {get;set;}
        [Required]
        public string Street {get;set;}
        [Required]
        public string City {get;set;}
        [Required]
        public string District {get;set;}
        [Required]
        public string State {get;set;}
        [Required]
        public string Country {get;set;}
        [Required]
        public int Zipcode {get;set;}
        [Required]
        public int EstablishmentNumber {get;set;}
    }
}
