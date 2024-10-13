using AutoMapper;
using BusinessLogic.DTOs;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Helpers
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<Car, CarDto>()
                .ForMember(carDto=>carDto.CategoryName,option=>option.MapFrom(car=>car.Category!.Name));
            CreateMap<Car, CarDto>();
        }
    }
}
