﻿using System;
using System.Collections.Generic;

namespace EvoCafe.DAL.Models
{
    public class Menu: EntityBase
    {
        public DateTime CreatedAt { get; set; }
        public ICollection<Dish> ActualDishes { get; set; }
    }
}