using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LifeManager.Data.Entities;

namespace LifeManager.Data.Repositories
{
    public class ListRepository : IListRepository
    {
        public async Task Add(List list)
        {
            throw new NotImplementedException();
        }

        public async Task Update(List list)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<List>> Get(Guid? id, string name)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<List>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
