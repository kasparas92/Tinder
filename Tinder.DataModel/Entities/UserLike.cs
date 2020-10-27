using System;
using System.Collections.Generic;
using System.Text;

namespace Tinder.DataModel.Entities
{
    public class UserLike
    {
        public User SourceUser { get; set; }
        public int SourceUserId { get; set; }
        public User LikedUser { get; set; }
        public int LikedUserId { get; set; }
    }
}
