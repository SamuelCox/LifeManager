using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace LifeManager.Messages.Calendar
{
    [ExcludeFromCodeCoverage]
    public class PersonDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
