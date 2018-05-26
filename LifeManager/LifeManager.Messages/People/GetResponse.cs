using System.Collections.Generic;
using LifeManager.Models;

namespace LifeManager.Messages.People
{
    public class GetResponse
    {
        public IEnumerable<PersonModel> People { get; set; }
    }
}
