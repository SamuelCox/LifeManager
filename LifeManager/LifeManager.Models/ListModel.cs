﻿using System;
using System.Collections.Generic;

namespace LifeManager.Models
{
    public class ListModel
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public List<string> Items { get; set; }
    }
}
