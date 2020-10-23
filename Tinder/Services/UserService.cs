using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tinder.API.Services.Interfaces;
using Tinder.DataModel.Entities;
using Tinder.DataModel.Repositories.Interfacies;

namespace Tinder.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public Task<bool> UpdateAsync(User user)
        {
            return _userRepository.UpdateAsync(user);
        }
    }
}
