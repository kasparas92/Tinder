using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tinder.DataModel.Entities;
using Tinder.ServiceModel.Dtos.Responses;

namespace Tinder.DataModel.Repositories.Interfacies
{
    public interface ILikeRepository
    {
        Task<UserLike> GetUserLike(int sourceId, int likedUserId);
        Task<User> GetUserWithLikes(int userId);
        Task<IEnumerable<LikeDto>> GetUserLikes(string predicate, int userId);
    }
}
