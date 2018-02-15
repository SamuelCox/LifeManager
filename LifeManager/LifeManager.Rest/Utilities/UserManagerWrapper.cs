using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using LifeManager.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LifeManager.Rest.Utilities
{
    [ExcludeFromCodeCoverage]
    public class UserManagerWrapper : UserManager<User>, IUserManagerWrapper
    {
        public UserManagerWrapper(IUserStore<User> userStore, IOptions<IdentityOptions> options, IPasswordHasher<User> hasher,
            IEnumerable<IUserValidator<User>> userValidators, IEnumerable<IPasswordValidator<User>> passwordValidators,ILookupNormalizer normalizer,
            IdentityErrorDescriber errorDescriber, IServiceProvider serviceProvider, ILogger<UserManager<User>> logger) :
            base(userStore, options, hasher, userValidators, passwordValidators, normalizer, errorDescriber, serviceProvider, logger)
        { 
        }

    }
}
