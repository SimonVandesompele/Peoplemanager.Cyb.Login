using Microsoft.AspNetCore.Mvc;
using PeopleManager.Model;
using PeopleManager.Services;
using PeopleManager.Services.Model.Requests;

namespace PeopleManager.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly PersonService _personService;

        public PeopleController(PersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<IActionResult> Find()
        {
            var result = await _personService.Find();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _personService.Get(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PersonRequest person)
        {
            var result = await _personService.Create(person);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, PersonRequest person)
        {
            var result = await _personService.Update(id, person);
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _personService.Delete(id);
            return Ok();
        }
    }
}
