using Microsoft.AspNetCore.Mvc;
using VNA_Table.Models;
using System.Text.Json;

namespace VNA_Table.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}