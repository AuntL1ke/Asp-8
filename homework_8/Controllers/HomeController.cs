using DataAccess.Data;
using DataAccess.Models;
using homework_8.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace homework_8.Controllers
{
    public class HomeController : Controller
    {
        private readonly CarDbContext _context;

        public HomeController(CarDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Car> cars = _context.Cars.Include(car=>car.Category).ToList();
            return View(cars);
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
