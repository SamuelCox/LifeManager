using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LifeManager.Data.Entities
{
    [ExcludeFromCodeCoverage]
    public class Person : IMongoEntity
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public DateTime? Birthday { get; set; }
        public Dictionary<string, string> ImportantNotes { get; set; }
    }
}
