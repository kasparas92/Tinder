using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tinder.ServiceModel.Dtos.Requests
{
    public class RegisterDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string KnownAs { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 4)]
        public string Password { get; set; }
        [Required]
        public string  Gender { get; set; }
        [Required]
        public string Country { get; set; }
    }
}
