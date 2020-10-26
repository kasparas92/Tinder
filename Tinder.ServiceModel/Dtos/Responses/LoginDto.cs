using System;
using System.Collections.Generic;
using System.Text;

namespace Tinder.ServiceModel.Dtos.Responses
{
    public class LoginDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public string PhotoUrl { get; set; }
        public string Gender { get; set; }
    }
}
