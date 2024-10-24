using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomewoirkWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICarService _carService;
        public CarsController(ICarService carService, IMapper mapper)
        {
            _carService = carService;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            List<CarDto> cars = _carService.GetAll();
            return Ok(cars);
        }
        [HttpGet("{id}")]
        public IActionResult GetbyId(int id) 
        {
            CarDto car = _carService.GetById(id);
            return Ok(car);
        }
        [HttpPost]
        public IActionResult CreatePost(CarDtoApi car) 
        {
            CarDto carDto = _mapper.Map<CarDto>(car);
            _carService.Add(carDto);
            return Ok(carDto);
        }
        [HttpPut("{id}")]

        public IActionResult EditPost(int id, CarDto carDto)
        {
            if (id != carDto.Id)
            {
                return BadRequest("Id в тілі запиту не відповідає Id у маршруті");
            }

            _carService.Update(carDto);
            return Ok(carDto);
        }

        [HttpDelete("{id}")]

        public IActionResult Delete(int id) 
        {
            CarDto car = _carService.GetById(id);
            _carService.Delete(car);
            return Ok();
        }
    }
}
