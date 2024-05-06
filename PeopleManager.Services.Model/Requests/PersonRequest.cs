using System.ComponentModel.DataAnnotations;

namespace PeopleManager.Services.Model.Requests
{
    public class PersonRequest
    {
        [Required]
        [Display(Name = "First name")]
        public required string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public required string LastName { get; set; }

        [EmailAddress]
        [Display(Name = "Email address")]
        public string? Email { get; set; }

    }
}
