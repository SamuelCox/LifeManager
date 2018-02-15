using System;
using System.Collections.Generic;
using System.Text;

namespace LifeManager.Data.Entities
{
    public interface IMongoEntity
    {
        Guid Id { get; }

        string UserId { get; }
    }
}
