using Microsoft.AspNetCore.Mvc;
using PeopleManager.Model;
using PeopleManager.Services;
using PeopleManager.Services.Model.Requests;

namespace PeopleManager.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentsController : ControllerBase
    {
        private readonly AssignmentService _assignmentService;

        public AssignmentsController(AssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }

        [HttpGet]
        public async Task<IActionResult> Find()
        {
            var result = await _assignmentService.Find();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _assignmentService.Get(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AssignmentRequest assignment)
        {
            var result = await _assignmentService.Create(assignment);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, AssignmentRequest assignment)
        {
            var result = await _assignmentService.Update(id, assignment);
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _assignmentService.Delete(id);
            return Ok();
        }
    }
}
