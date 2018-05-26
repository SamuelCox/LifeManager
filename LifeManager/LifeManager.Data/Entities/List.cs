using System;
using System.Collections.Generic;

namespace LifeManager.Data.Entities
{
    public class List : IMongoEntity
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public List<string> Items { get; set; }
    }
}
