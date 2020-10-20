using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tinder.DataModel.Context;
using Tinder.DataModel.Entities;
using Tinder.DataModel.Repositories.Interfacies;

namespace Tinder.DataModel.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TinderContext _tinderContext;
        public UserRepository(TinderContext tinderContext)
        {
            _tinderContext = tinderContext;
        }
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _tinderContext.User
                .Include(x => x.Photos)
                .ToListAsync(); ;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _tinderContext.User
                .Where(x => x.Id == id)
                .Include(x => x.Photos)
                .FirstOrDefaultAsync();
        }
    }
}
