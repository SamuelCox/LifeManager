using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LifeManager.Data.Entities;

namespace LifeManager.Data.Repositories
{
    public interface IPeopleRepository
    {
        Task Add(Person person);
        Task Update(Person person);
        Task Delete(Guid id, string userId);
        Task<IEnumerable<Person>> Get(Guid? id, string userId, string name, DateTime? birthday);
        Task<IEnumerable<Person>> GetAll(string userId);
    }
}
