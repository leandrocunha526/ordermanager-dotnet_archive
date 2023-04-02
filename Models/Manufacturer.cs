using System.ComponentModel.DataAnnotations;

namespace ordermanager_dotnet.Models
{
    public class Manufacturer
    {
        [Required]
        public int Id {get;set;}

        [Required]
        public string Description {get;set;}
    }
}
