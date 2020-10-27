using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tinder.DataModel.Entities;
using Tinder.ServiceModel.Dtos.Responses;

namespace Tinder.API.Services.Interfaces
{
    public interface ILikeService
    {
        Task<UserLike> GetUserLike(int sourceId, int likedUserId);
        Task<User> GetUserWithLikes(int userId);
        Task<IEnumerable<LikeDto>> GetUserLikes(string predicate, int userId);
    }
}
