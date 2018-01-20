using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using LifeManager.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LifeManager.Data.Contexts
{
    [ExcludeFromCodeCoverage]
    public class AuthContext : DbContext
    {
        public AuthContext()
        {
            
        }

        public AuthContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        
    }
}
