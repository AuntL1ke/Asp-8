using DataAccess.Models;

using DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Validators;
using Microsoft.AspNetCore.Authorization;
using BusinessLogic.DTOs;
using BusinessLogic.Services;
using FluentValidation;
using AutoMapper;

namespace homework_8.Controllers
{
    [Authorize(Roles ="Admin")]
    public class CarsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly CarService _carService;
        private readonly CarValidator _validator;
        public CarsController(CarService carService, CarValidator validator, IMapper mapper) 
        { 
        
            _carService = carService;
            _validator = validator;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            List<CarDto> cars = _carService.GetAll();
            return View(cars);
        }
        public IActionResult Create() 
        { 

            return View();
        }
        [HttpPost]
        public IActionResult Create(CarDto carDto)
        {
            Car car = _mapper.Map<Car>(carDto);
            if (car == null) { return BadRequest(); }
            if (car.Model.Length<=1)
            {
                ModelState.AddModelError("All", "Field cannot be empty");
            }
            var result = _validator.Validate(car);
            if (ModelState.IsValid&&result.IsValid) { _carService.Add(carDto);  }
            else { return View(car); }

            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            CarDto car = _carService.GetById(id);
            return View(car);
        }
        [HttpPost]
        public IActionResult Edit(CarDto carDto)
        {
            
            if (carDto == null) { return BadRequest(); }
            if (ModelState.IsValid) { _carService.Update(carDto); }
            else { return View(carDto); }

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            CarDto car = _carService.GetById(id);
            return View(car);
        }

        public IActionResult Remove(int id)
        {
      
            _carService.Delete(id);
            return RedirectToAction("Index");



        }

    }
}
