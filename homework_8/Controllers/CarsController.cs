using DataAccess.Models;

using DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using homework_8.Validators;
using Microsoft.AspNetCore.Authorization;

namespace homework_8.Controllers
{
    [Authorize(Roles ="Admin")]
    public class CarsController : Controller
    {

        private readonly CarDbContext _context;
        private readonly CarValidator _validator;
        public CarsController(CarDbContext context, CarValidator validator) 
        { 
        
            _context = context;
            _validator = validator;
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
            var result = _validator.Validate(car);
            if (ModelState.IsValid&&result.IsValid) { _context.Add(car); _context.SaveChanges(); }
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
