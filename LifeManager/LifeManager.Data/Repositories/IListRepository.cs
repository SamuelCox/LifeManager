using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LifeManager.Data.Entities;

namespace LifeManager.Data.Repositories
{
    public interface IListRepository
    {
        Task Add(List list);
        Task Update(List list);
        Task Delete(Guid id);
        Task<IEnumerable<List>> Get(Guid? id, string name);
        Task<IEnumerable<List>> GetAll();
    }
}
