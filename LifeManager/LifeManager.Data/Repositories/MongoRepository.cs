using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LifeManager.Data.Entities;
using MongoDB.Driver;
using Remotion.Linq.Clauses;

namespace LifeManager.Data.Repositories
{
    public class MongoRepository<T> where T : IMongoEntity
    {
        private readonly IMongoDatabase _db;
        private readonly string _collectionName;

        public MongoRepository(IMongoDatabase database, string collectionName)
        {
            _db = database;
            _collectionName = collectionName;
        }

        public virtual async Task Add(T entity)
        {
            var entities = _db.GetCollection<T>(_collectionName);
            var exists = await EntityExists(entity.Id, entities);
            if (!exists)
            {
                await entities.InsertOneAsync(entity);                
            }
        }

        public virtual async Task Update(T entity)
        {
            var entities = _db.GetCollection<T>(_collectionName);
            var filter = Builders<T>.Filter.Eq(x => x.Id, entity.Id);
            await entities.UpdateOneAsync(filter, new ObjectUpdateDefinition<T>(entity));
        }

        public virtual async Task Delete(Guid id)
        {
            var calendarEvents = _db.GetCollection<T>(_collectionName);
            var filter = Builders<T>.Filter.Eq(x => x.Id, id);
            await calendarEvents.DeleteOneAsync(filter);
        }

        public virtual async Task<IEnumerable<T>> Get(Guid? id)
        {
            var entities = await _db.GetCollection<T>(_collectionName).FindAsync(Builders<T>.Filter.Eq(x => x.Id, id));
            return entities.ToList();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var entities = await _db.GetCollection<T>(_collectionName).FindAsync(Builders<T>.Filter.Empty);
            return entities.ToList();
        }

        private async Task<bool> EntityExists(Guid listId, IMongoCollection<T> collection)
        {
            var filter = Builders<T>.Filter.Eq(x => x.Id, listId);
            var lists = await collection.FindAsync(filter);
            if (lists.ToList().Count != 0)
            {
                return true;
            }

            return false;
        }
    }
}
