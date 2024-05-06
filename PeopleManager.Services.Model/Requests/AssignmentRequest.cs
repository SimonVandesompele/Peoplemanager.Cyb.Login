using System.ComponentModel.DataAnnotations;

namespace PeopleManager.Services.Model.Requests
{
    public class AssignmentRequest
    {
        [Required]
        public required string Name { get; set; }

        public string? Description { get; set; }

        public int? AssignedToId { get; set; }

    }
}
