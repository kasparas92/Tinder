using System;
using System.Collections.Generic;
using System.Text;

namespace Tinder.ServiceModel.Dtos.Responses
{
    public class UserUpdateDto
    {
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string Country { get; set; }
    }
}
