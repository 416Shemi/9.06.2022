﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nest_BackFront.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public bool IsDeleted { get; set; }
        public List<Product> Products { get; set; }
    }
}
