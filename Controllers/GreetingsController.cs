// This "using" imports the ASP.NET Core MVC Framework
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
//A Controller class must always end in "Controller"
//Namespace matches the folder structure: Controllers/GreetingController.cs
namespace StudentPortal.Controllers
{
    public class GreetingController : Controller
    {
        //Every public method on a Controller is an "action" - a unit of work that can
        // be reached by a URL.
        public IActionResult Index()
        {
            //Returns Views/Greeting/Index.cshtml by convention
            return View();
        }
            public IActionResult Hello(string name)
        {
            //ViewBag is a dynamic bag we can dump data into for the view to read.
            ViewBag.Name = name ?? "Student";

            return View();
        }
        }
    }