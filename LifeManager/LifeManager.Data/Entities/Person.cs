using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace LifeManager.Data.Entities
{
    [ExcludeFromCodeCoverage]
    public class Person : IMongoEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
