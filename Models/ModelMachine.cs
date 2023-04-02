using System.ComponentModel.DataAnnotations;

namespace ordermanager_dotnet.Models
{
    // Model of the agricultural machine not declare only model a location or parameter
    // named "model" cannot be declared in this scope because that name is used in
    // a location delimiting scope to define a location or parameter.
    // Error (CS0136) consult https://docs.microsoft.com/en-us/dotnet/csharp/misc/cs0136.
    public class ModelMachine
    {
        [Required]
        public int Id {get;set;}

        [Required]
        public string Description {get;set;}

        [Required]
        public int ManufacturerId {get;set;}

        public Manufacturer Manufacturers {get;set;}
    }
}
