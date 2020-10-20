using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tinder.DataModel.Entities;

namespace Tinder.API.Services.Interfaces
{
    public interface IAccountService
    {
        Task<User> Register(string name, string password, string gender, string country);
        Task<User> Login(string name, string password);
        Task<bool> IsUserExist(string name);
    }
}
