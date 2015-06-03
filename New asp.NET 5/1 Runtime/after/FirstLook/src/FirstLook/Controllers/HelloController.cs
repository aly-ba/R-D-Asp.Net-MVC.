using FirstLookLibrary;
using Microsoft.AspNet.Mvc;
using System.Reflection;

namespace FirstLook.Controllers
{
    public class HelloController : Controller
    {
        public string Index()
        {

            var greeter = new Greeter();
            var greeting = greeter.GetGreeting();

#if ASPNET50
            return Assembly.GetExecutingAssembly().FullName;
#endif
#if ASPNETCORE50
            return typeof(HelloController).AssemblyQualifiedName;
#endif
        }
    }
}