﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string IdsProduct {  get; set; }
        public decimal TotalPrice {  get; set; }
        public string UserId {  get; set; }
    }
}
