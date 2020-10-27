using System.Collections.Generic;
using System.Threading.Tasks;
using Tinder.API.Services.Interfaces;
using Tinder.DataModel.Entities;
using Tinder.DataModel.Repositories.Interfacies;
using Tinder.ServiceModel.Dtos.Responses;

namespace Tinder.API.Services
{
    public class LikeService : ILikeService
    {
        private readonly ILikeRepository _likeRepository;
        public LikeService(ILikeRepository likeRepository)
        {
            _likeRepository = likeRepository;
        }
        public async Task<UserLike> GetUserLike(int sourceId, int likedUserId)
        {
            return await _likeRepository.GetUserLike(sourceId, likedUserId);
        }

        public async Task<IEnumerable<LikeDto>> GetUserLikes(string predicate, int userId)
        {
            return await _likeRepository.GetUserLikes(predicate, userId);
        }

        public async Task<User> GetUserWithLikes(int userId)
        {
            return await _likeRepository.GetUserWithLikes(userId);
        }
    }
}
