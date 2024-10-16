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
        private readonly CarDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        public CarService(CarDbContext context, IMapper mapper,IFileService fileService)
        {
            _context = context;
            _mapper = mapper;   
            _fileService = fileService;
        }
        //public void Add(CarDto carDto)
        //{
        //    if (carDto.Image != null)
        //        carDto.ImagePath =
        //        fileService.SaveProductImage(carDto.Image).Result;
        //    Car car = mapper.Map<Car>(carDto);
        //    context.Cars.Add(car);
        //    context.SaveChanges();
        //}

        //public void Delete(int id)
        //{

        //    Car car = context.Cars.FirstOrDefault(x => x.Id == id);
        //    context.Cars.Remove(car);
        //    context.SaveChanges();
        //    if (car.ImagePath != null)
        //        fileService.DeleteProductImage(car.ImagePath);

        //}
        public void Add(CarDto carDto)
        {
            if (carDto != null)
            {
                if (carDto.Image != null)
                {
                    carDto.ImagePath = _fileService.SaveProductImage(carDto.Image).Result;
                }

                Car car = _mapper.Map<Car>(carDto);

                _context.Cars.Add(car);
                _context.SaveChanges();
            }
        }

        public void Update(CarDto carDto)
        {
            CarDto carOld = GetById(carDto.Id);

            if (carOld != null && carDto != null)
            {
                if (carDto.Image != null)
                {
                    carDto.ImagePath = _fileService.EditProductImage(carDto.ImagePath!, carDto.Image).Result;
                }

                Car car = _mapper.Map<Car>(carDto);

                _context.Cars.Update(car);
                _context.SaveChanges();
            }
        }

        public void Delete(CarDto carDto)
        {
            if (carDto != null)
            {
                if (carDto.ImagePath != null)
                {
                    _fileService.DeleteProductImage(carDto.ImagePath);
                }

                Car car = _mapper.Map<Car>(carDto);

                _context.Cars.Remove(car);
                _context.SaveChanges();
            }
        }

        public List<CarDto> GetAll()
        {
            List<Car> cars = _context.Cars.Include(car=>car.Category).ToList();
            return _mapper.Map<List<CarDto>>(cars);
        }

        public CarDto GetById(int id)
        {
            Car car = _context.Cars.Include(car=>car.Category).FirstOrDefault(x=>x.Id == id);
            return _mapper.Map<CarDto>(car);
        }

        //public void Update(CarDto carDto)
        //{
        //    if (carDto.Image != null)
        //        carDto.ImagePath =
        //        fileService.EditProductImage(carDto.ImagePath, carDto.Image).Result;
        //    Car car = mapper.Map<Car>(carDto);
        //    context.Cars.Update(car);
        //    context.SaveChanges();
        //}
    }
}
