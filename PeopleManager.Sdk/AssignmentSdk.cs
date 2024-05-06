using System.Net.Http.Json;
using PeopleManager.Model;

namespace PeopleManager.Sdk
{
    public class AssignmentSdk
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AssignmentSdk(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IList<Assignment>> Find()
        {
            var httpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            var route = "/api/assignments";
            var response = await httpClient.GetAsync(route);

            response.EnsureSuccessStatusCode();

            var assignments = await response.Content.ReadFromJsonAsync<IList<Assignment>>();

            if (assignments is null)
            {
                return new List<Assignment>();
            }

            return assignments;
        }

        //Get
        public async Task<Assignment?> Get(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            var route = $"/api/assignments/{id}";
            var response = await httpClient.GetAsync(route);

            response.EnsureSuccessStatusCode();

            var assignment = await response.Content.ReadFromJsonAsync<Assignment>();

            return assignment;
        }

        //Create
        public async Task<Assignment?> Create(Assignment assignment)
        {
            var httpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            var route = "/api/assignments";
            var response = await httpClient.PostAsJsonAsync(route, assignment);

            response.EnsureSuccessStatusCode();

            var createdAssignment = await response.Content.ReadFromJsonAsync<Assignment>();

            return createdAssignment;
        }

        //Update
        public async Task<Assignment?> Update(int id, Assignment assignment)
        {
            var httpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            var route = $"/api/assignments/{id}";
            var response = await httpClient.PutAsJsonAsync(route, assignment);

            response.EnsureSuccessStatusCode();

            var updatedAssignment = await response.Content.ReadFromJsonAsync<Assignment>();

            return updatedAssignment;
        }

        //Delete
        public async Task Delete(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            var route = $"/api/assignments/{id}";
            var response = await httpClient.DeleteAsync(route);

            response.EnsureSuccessStatusCode();
        }
    }
}
