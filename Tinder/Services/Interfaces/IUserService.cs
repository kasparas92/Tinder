using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tinder.DataModel.Entities;

namespace Tinder.API.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetByGenderAsync(string gender);
        Task<bool> UpdateAsync(User user);
    }
}
