using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LifeManager.Models;

namespace LifeManager.PeopleService.Services
{
    public interface IPeopleService
    {
        Task CreatePerson(PersonModel model);
        Task UpdatePerson(PersonModel model);
        Task DeletePerson(Guid id, string userId);
        Task<IEnumerable<PersonModel>> GetPerson(PersonModel model);
        Task<IEnumerable<PersonModel>> GetAllPeople(string userId);
    }
}
