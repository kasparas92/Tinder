using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tinder.DataModel.Entities;
using Tinder.ServiceModel.Dtos.Requests;

namespace Tinder.API.Services.Interfaces
{
    public interface IAccountService
    {
        Task<User> Register(RegisterDto register);
        Task<User> Login(string name, string password);
        Task<bool> IsUserExist(string name);
    }
}
