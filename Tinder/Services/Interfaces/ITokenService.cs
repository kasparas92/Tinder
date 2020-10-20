using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tinder.DataModel.Entities;

namespace Tinder.API.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
