using Microsoft.AspNetCore.Mvc;
using PeopleManager.Cyb.Ui.Mvc.Models;
using System.Diagnostics;
using PeopleManager.Sdk;

namespace PeopleManager.Cyb.Ui.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly PersonSdk _personSdk;

        public HomeController(PersonSdk personSdk)
        {
            _personSdk = personSdk;
        }
        public async Task<IActionResult> Index()
        {
            var people = await _personSdk.Find();
            return View(people);
        }

        public async Task<IActionResult> Details(int id)
        {
            var person = await _personSdk.Get(id);
            if (person is null)
            {
                return RedirectToAction("Index");
            }
            return View(person);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}