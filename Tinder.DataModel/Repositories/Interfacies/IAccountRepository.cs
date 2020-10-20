using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tinder.DataModel.Entities;

namespace Tinder.DataModel.Repositories.Interfacies
{
    public interface IAccountRepository
    {
        Task<User> Register(User user);
        Task<User> Login(string name, string password);
        Task<bool> IsUserExist(string name);
    }
}
