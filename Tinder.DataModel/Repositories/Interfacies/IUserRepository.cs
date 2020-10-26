using System.Collections.Generic;
using System.Threading.Tasks;
using Tinder.DataModel.Entities;

namespace Tinder.DataModel.Repositories.Interfacies
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetByGenderAsync(string gender);
        Task<bool> UpdateAsync(User user);
    }
}
