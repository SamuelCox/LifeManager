using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LifeManager.Data.Entities;
using MongoDB.Driver;

namespace LifeManager.Data.Repositories
{
    public class ListRepository : MongoRepository<List>, IListRepository
    {
        private readonly IMongoDatabase _db;

        public ListRepository(IMongoDatabase database) : base(database, "Lists")
        {
            _db = database;
        }        

        public async Task<IEnumerable<List>> Get(Guid? id, string name)
        {
            var filter = Builders<List>.Filter.Empty;
            if (id.HasValue)
            {
                var idFilter = Builders<List>.Filter.Eq(x => x.Id, id);
                filter = Builders<List>.Filter.And(filter, idFilter);
            }

            if (!string.IsNullOrEmpty(name))
            {
                var nameFilter = Builders<List>.Filter.Eq(x => x.Name, name);
                filter = Builders<List>.Filter.And(filter, nameFilter);
            }
            var lists = await _db.GetCollection<List>("Lists").FindAsync(filter);
            return lists.ToList();
        }                
    }
}
