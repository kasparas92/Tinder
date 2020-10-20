using System;
using System.Collections.Generic;

namespace Tinder.ServiceModel.Dtos.Responses
{
    public class UserDto
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime Created { get; set; }
        public string Gender { get; set; }
        public string Country { get; set; }
        public ICollection<PhotoDto> Photos { get; set; }
    }
}
