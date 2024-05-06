using System.ComponentModel.DataAnnotations;

namespace PeopleManager.Services.Model.Requests
{
    public class UserRegisterRequest
    {
        [Required]
        [EmailAddress]
        public required string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public required string Password { get; set; }
    }
}
