using System;
using System.Collections.Generic;
using System.Text;

namespace Tinder.ServiceModel.Dtos.Requests
{
    public class RegisterDto
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string  Gender { get; set; }
        public string Country { get; set; }
    }
}
