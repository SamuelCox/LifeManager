﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LifeManager.Data.Entities
{
    public class List
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<string> Items { get; set; }
    }
}