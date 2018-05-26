using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LifeManager.Models;

namespace LifeManager.ListsService.Services
{
    public interface IListService
    {
        Task CreateList(ListModel model);
        Task UpdateList(ListModel model);
        Task DeleteList(Guid id, string userId);
        Task<IEnumerable<ListModel>> GetList(ListModel model);
        Task<IEnumerable<ListModel>> GetAllLists(string userId);
    }
}
