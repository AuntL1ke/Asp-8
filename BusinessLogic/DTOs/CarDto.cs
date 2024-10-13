﻿using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs
{
    public class CarDto
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public short Year { get; set; }

        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
}
