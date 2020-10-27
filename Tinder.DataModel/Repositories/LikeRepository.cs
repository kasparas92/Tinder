using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tinder.DataModel.Context;
using Tinder.DataModel.Entities;
using Tinder.DataModel.Repositories.Interfacies;
using Tinder.ServiceModel.Dtos.Responses;

namespace Tinder.DataModel.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly TinderContext _tinderContext;
        public LikeRepository(TinderContext tinderContext)
        {
            _tinderContext = tinderContext;
        }
        public async Task<UserLike> GetUserLike(int sourceId, int likedUserId)
        {
            return await _tinderContext.Like
                .FindAsync(sourceId, likedUserId);
        }

        public async Task<IEnumerable<LikeDto>> GetUserLikes(string predicate, int userId)
        {
            var users = _tinderContext.User
                .OrderBy(u => u.Id)
                .AsQueryable();
            var likes = _tinderContext.Like
                .AsQueryable();

            if (predicate == "liked")
            {
                likes = likes.Where(l => l.SourceUserId == userId);
                users = likes.Select(l => l.LikedUser);
            }
            if (predicate == "likedBy")
            {
                likes = likes.Where(l => l.LikedUserId == userId);
                users = likes.Select(l => l.SourceUser);
            }

            return await users.Select(user => new LikeDto
            {
                Name = user.Name,
                KnownAs = user.KnowAs,
                Country = user.Country,
                PhotoUrl = user.Photos.FirstOrDefault(p => p.IsMain).Url,
                Id = user.Id
            }).ToListAsync();
        }

        public async Task<User> GetUserWithLikes(int userId)
        {
            return await _tinderContext.User
                .Include(x => x.LikedUsers)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }
    }
}
