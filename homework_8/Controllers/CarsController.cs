using DataAccess.Models;

using DataAccess.Data;
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
        public IActionResult Create() 
        { 

            return View();
        }
        [HttpPost]
        public IActionResult Create(Car car)
        {

            if (car == null) { return BadRequest(); }
            if (car.Model.Length<=1)
            {
                ModelState.AddModelError("All", "Field cannot be empty");
            }
            if (ModelState.IsValid) { _context.Add(car); _context.SaveChanges(); }
            else { return View(car); }

            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            Car car = _context.Cars.FirstOrDefault(c => c.Id == id)!;
            return View(car);
        }
        [HttpPost]
        public IActionResult Edit(Car car)
        {
            if (car == null) { return BadRequest(); }
            if (ModelState.IsValid) { _context.Update(car); _context.SaveChanges(); }
            else { return View(car); }

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            Car car = _context.Cars.FirstOrDefault(c=>c.Id == id);
            return View(car);
        }

        public IActionResult Remove(int id)
        {
            Car car = _context.Cars.FirstOrDefault(c => c.Id == id);

            _context.Cars.Remove(car);
            _context.SaveChanges();
            return RedirectToAction("Index");


        }

    }
}
