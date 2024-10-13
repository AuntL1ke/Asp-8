using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class CarService : ICarService
    {
        private readonly CarDbContext context;
        private readonly IMapper mapper;
        public CarService(CarDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;   
        }
        public void Add(CarDto carDto)
        {
            Car car = mapper.Map<Car>(carDto);
            context.Cars.Add(car);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            Car car = context.Cars.FirstOrDefault(x => x.Id == id);
            context.Cars.Remove(car);
            context.SaveChanges();
        }

        public List<CarDto> GetAll()
        {
            List<Car> cars = context.Cars.Include(car=>car.Category).ToList();
            return mapper.Map<List<CarDto>>(cars);
        }

        public CarDto GetById(int id)
        {
            Car car = context.Cars.Include(car=>car.Category).FirstOrDefault(x=>x.Id == id);
            return mapper.Map<CarDto>(car);
        }

        public void Update(CarDto carDto)
        {
            Car car = mapper.Map<Car>(carDto);
            context.Cars.Update(car);
            context.SaveChanges();
        }
    }
}
