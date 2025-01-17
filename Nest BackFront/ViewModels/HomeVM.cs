﻿using Nest_BackFront.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nest_BackFront.ViewModels
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
        public List<Product> RecentProducts { get; set; }
        public List<Product> TopRatedProducts { get; set; }
    }
}
