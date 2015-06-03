using Messenger.Services;
using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Messenger.Controllers
{
    [Route("/[controller]"), Route("/")]
    public class HomeController : Controller
    {
        IGreetingService _greeter;
        public HomeController(IGreetingService greeter)
        {
            _greeter = greeter;
        }

        [Route("[action]"), Route("")]
        public IActionResult Default()
        {
            var greeting = _greeter.GetTodaysGreeting();
            return View(greeting);
        }
    }
}
