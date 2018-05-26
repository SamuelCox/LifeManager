using System;

namespace LifeManager.Data.Entities
{
    public interface IMongoEntity
    {
        Guid Id { get; }

        string UserId { get; }
    }
}
