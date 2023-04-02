using System.ComponentModel.DataAnnotations;

namespace ordermanager_dotnet.Models
{
    public class AuthenticateModel
    {
        [Required]
        public string Username {get;set;}

        [Required]
        public string Password {get;set;}
    }
}
