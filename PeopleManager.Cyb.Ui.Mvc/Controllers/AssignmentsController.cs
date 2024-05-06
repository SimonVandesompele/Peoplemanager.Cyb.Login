using Microsoft.AspNetCore.Mvc;
using PeopleManager.Model;
using PeopleManager.Sdk;

namespace PeopleManager.Cyb.Ui.Mvc.Controllers
{
    //[Authorize]
    public class AssignmentsController : Controller
    {
        private readonly AssignmentSdk _assignmentSdk;
        private readonly PersonSdk _personSdk;

        public AssignmentsController(
            AssignmentSdk assignmentSdk,
            PersonSdk personSdk)
        {
            _assignmentSdk = assignmentSdk;
            _personSdk = personSdk;
        }

        public async Task<IActionResult> Index()
        {
            var assignments = await _assignmentSdk.Find();
            return View(assignments);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return await AssignmentView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Assignment assignment)
        {
            if (!ModelState.IsValid)
            {
                return await AssignmentView(assignment);
            }

            await _assignmentSdk.Create(assignment);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute]int id)
        {
            var assignment = await _assignmentSdk.Get(id);
            return await AssignmentView(assignment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int id, Assignment assignment)
        {
            if (!ModelState.IsValid)
            {
                return await AssignmentView(assignment);
            }

            await _assignmentSdk.Update(id, assignment);

            return RedirectToAction("Index");
        }

        private async Task<IActionResult> AssignmentView(Assignment? assignment = null)
        {
            var people = await _personSdk.Find();
            //ViewData["People"] = people;
            ViewBag.People = people;

            if (assignment is null)
            {
                return View();
            }

            return View(assignment);
        }

        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var assignment = await _assignmentSdk.Get(id);
            return View(assignment);
        }

        [HttpPost("/assignments/delete/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([FromRoute]int id)
        {
            await _assignmentSdk.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
