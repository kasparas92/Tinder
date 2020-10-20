using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Tinder.DataModel.Context;
using Tinder.DataModel.Entities;
using Tinder.DataModel.Repositories.Interfacies;

namespace Tinder.DataModel.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly TinderContext _tinderContext;

        public AccountRepository(TinderContext tinderContext)
        {
            _tinderContext = tinderContext;
        }
        public async Task<bool> IsUserExist(string name)
        {
            return await _tinderContext.User
                .AnyAsync(x => x.Name.ToLower() == name.ToLower());
        }

        public async Task<User> Login(string name, string password)
        {
            var user = await _tinderContext.User
                .Include(p => p.Photos)
                .FirstOrDefaultAsync(x => x.Name == name);
            if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }
            return user;
        }

        public async Task<User> Register(User user)
        {
            _tinderContext.User.Add(user);
            await _tinderContext.SaveChangesAsync();
            return user;
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            }
            return true;

        }
    }
}
