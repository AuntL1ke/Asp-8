using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs
{
    public class CarDtoApi
    {
        public string Model { get; set; }
        public string Color { get; set; }
        public short Year { get; set; }
        public string? ImagePath { get; set; }
       

        public int CategoryId { get; set; }

    }
}
