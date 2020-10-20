using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tinder.DataModel.Entities;

namespace Tinder.DataModel.Context
{
    public class TinderContext : DbContext
    {
        public TinderContext(DbContextOptions<TinderContext> options) : base(options)
        {

        }
        public DbSet<User>  User { get; set; }
    }
}
