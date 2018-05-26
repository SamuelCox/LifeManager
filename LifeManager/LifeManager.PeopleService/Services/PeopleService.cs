using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LifeManager.Data.Entities;
using LifeManager.Data.Repositories;
using LifeManager.Models;

namespace LifeManager.PeopleService.Services
{
    public class PeopleService : IPeopleService
    {
        private readonly IPeopleRepository _peopleRepository;

        public PeopleService(IPeopleRepository peopleRepository)
        {
            _peopleRepository = peopleRepository;
        }

        public async Task CreatePerson(PersonModel model)
        {
            var person = Mapper.Map<Person>(model);
            await _peopleRepository.Add(person);
        }

        public async Task UpdatePerson(PersonModel model)
        {
            var person = Mapper.Map<Person>(model);
            await _peopleRepository.Update(person);
        }

        public async Task DeletePerson(Guid id, string userId)
        {
            await _peopleRepository.Delete(id, userId);
        }

        public async Task<IEnumerable<PersonModel>> GetPerson(PersonModel model)
        {
            var people = await _peopleRepository.Get(model.Id, model.UserId, model.Name, model.Birthday);
            return Mapper.Map<IEnumerable<PersonModel>>(people);
        }

        public async Task<IEnumerable<PersonModel>> GetAllPeople(string userId)
        {
            var people = await _peopleRepository.GetAll(userId);
            return Mapper.Map<IEnumerable<PersonModel>>(people);
        }
    }
}
