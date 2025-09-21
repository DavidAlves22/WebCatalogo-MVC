using System.Diagnostics;
using CatalogoMvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoMvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
