﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Tinder.ServiceModel.Dtos.Responses
{
    public class UserDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Country { get; set; }
        public string Token { get; set; }
    }
}
