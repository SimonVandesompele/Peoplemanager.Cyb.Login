using Microsoft.AspNetCore.Mvc;
using PeopleManager.Model;
using PeopleManager.Sdk;

namespace PeopleManager.Cyb.Ui.Mvc.Controllers
{
    //[Authorize]
    public class PeopleController : Controller
    {
        private readonly PersonSdk _personSdk;

        public PeopleController(PersonSdk personSdk)
        {
            _personSdk = personSdk;
        }

        public async Task<IActionResult> Index()
        {
            var people = await _personSdk.Find();
            return View(people);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Person person)
        {
            if (!ModelState.IsValid)
            {
                return View(person);
            }

            await _personSdk.Create(person);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute]int id)
        {
            var person = await _personSdk.Get(id);
            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int id, Person person)
        {
            if (!ModelState.IsValid)
            {
                return View(person);
            }

            await _personSdk.Update(id, person);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var person = await _personSdk.Get(id);
            return View(person);
        }

        [HttpPost("/people/delete/{id:int?}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _personSdk.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
