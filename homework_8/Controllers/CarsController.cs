using homework_8.Data;
using homework_8.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace homework_8.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarDbContext _context;
        public CarsController(CarDbContext context) 
        { 
        
            _context = context;
        }
        public IActionResult Index()
        {
            List<Car> cars = _context.Cars.Include(car=>car.Category).ToList();
            return View(cars);
        }
    }
}
