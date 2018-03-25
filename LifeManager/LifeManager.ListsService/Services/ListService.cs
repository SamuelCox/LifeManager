using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LifeManager.Data.Entities;
using LifeManager.Data.Repositories;
using LifeManager.Models;

namespace LifeManager.ListsService.Services
{
    public class ListService : IListService
    {
        private readonly IListRepository _listRepository;

        public ListService(IListRepository listRepository)
        {
            _listRepository = listRepository;
        }

        public async Task CreateList(ListModel model)
        {
            model.Id = Guid.NewGuid();
            var list = Mapper.Map<List>(model);
            await _listRepository.Add(list);
        }

        public async Task UpdateList(ListModel model)
        {
            var list = Mapper.Map<List>(model);
            await _listRepository.Update(list);
        }

        public async Task DeleteList(Guid id, string userId)
        {
            await _listRepository.Delete(id, userId);
        }

        public async Task<IEnumerable<ListModel>> GetList(ListModel model)
        {
            var entities = await _listRepository.Get(model.Id, model.UserId, model.Name);
            return Mapper.Map<IEnumerable<ListModel>>(entities);
        }

        public async Task<IEnumerable<ListModel>> GetAllLists(string userId)
        {
            var entities = await _listRepository.GetAll(userId);
            return Mapper.Map<IEnumerable<ListModel>>(entities);
        }
    }
}
