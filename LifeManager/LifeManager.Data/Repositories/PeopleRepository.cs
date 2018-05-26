using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LifeManager.Data.Entities;
using MongoDB.Driver;

namespace LifeManager.Data.Repositories
{
    public class PeopleRepository : MongoRepository<Person>, IPeopleRepository
    {
        public PeopleRepository(IMongoDatabase database) : base(database, "people")
        {            
        }

        public async Task<IEnumerable<Person>> Get(Guid? id, string userId, string name, DateTime? birthday)
        {
            var filter = Builders<Person>.Filter.Empty;
            if (id.HasValue)
            {
                var idFilter = Builders<Person>.Filter.Eq(x => x.Id, id);
                filter = Builders<Person>.Filter.And(filter, idFilter);
            }

            var userIdFilter = Builders<Person>.Filter.Eq(x => x.UserId, userId);
            filter = Builders<Person>.Filter.And(filter, userIdFilter);

            if (!string.IsNullOrEmpty(name))
            {
                var nameFilter = Builders<Person>.Filter.Eq(x => x.Name, name);
                filter = Builders<Person>.Filter.And(filter, nameFilter);
            }

            if (birthday.HasValue)
            {
                var birthdayFilter = Builders<Person>.Filter.Eq(x => x.Birthday, birthday);
                filter = Builders<Person>.Filter.And(filter, birthdayFilter);
            }

            var people = await _db.GetCollection<Person>("people").FindAsync(filter);
            return people.ToList();
        }
    }
}
