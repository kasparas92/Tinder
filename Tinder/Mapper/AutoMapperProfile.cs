using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tinder.API.Extensions;
using Tinder.DataModel.Entities;
using Tinder.ServiceModel.Dtos.Requests;
using Tinder.ServiceModel.Dtos.Responses;

namespace Tinder.API.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Photo, PhotoDto>();
            CreateMap<User, UserDto>().ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => 
                        src.Photos.FirstOrDefault(x => x.IsMain).Url)).ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateofBirth.Age()));
            CreateMap<User, RegisterDto>();
            CreateMap<User, UserDetailsDto>();
            CreateMap<User, LoginDto>();
        }
    }
}
