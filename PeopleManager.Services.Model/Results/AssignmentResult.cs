namespace PeopleManager.Services.Model.Results
{
    public class AssignmentResult
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        public int? AssignedToId { get; set; }

        public string? AssignedToFirstName { get; set; }
        public string? AssignedToLastName { get; set; }
    }
}
