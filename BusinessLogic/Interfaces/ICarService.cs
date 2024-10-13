using BusinessLogic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface ICarService
    {
        void Add(CarDto carDto);
        void Update(CarDto carDto);
        void Delete(int id);
        CarDto GetById(int id);
        List<CarDto> GetAll();
    }
}
