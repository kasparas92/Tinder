using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tinder.API.Extensions;
using Tinder.API.Services.Interfaces;
using Tinder.DataModel.Entities;
using Tinder.ServiceModel.Dtos.Responses;

namespace Tinder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        public UserController(IUserService userService, IMapper mapper, IPhotoService photoService)
        {
            _userService = userService;
            _mapper = mapper;
            _photoService = photoService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllAsync()
        {
            var users = await _userService.GetAllAsync();
            var response = _mapper.Map<IEnumerable<UserDto>>(users);
            return Ok(response);
        }
        [HttpGet("{id}", Name = "GetUser")]
        public async Task<ActionResult<UserDto>> GetByIdAsync(int id)
        {
            var users = await _userService.GetByIdAsync(id);
            var response = _mapper.Map<UserDto>(users);
            return Ok(response);
        }
        [HttpPut("edit")]
        public async Task<ActionResult> UpdateUser(UserUpdateDto userUpdateDto)
        {
            var userId = await _userService.GetByIdAsync(User.GetUserId());
            var result = _mapper.Map(userUpdateDto, userId);

            var user = await _userService.UpdateAsync(result);

            if (user)
                return NoContent();
            return BadRequest("Updation Failed!!");
        }
        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhotoAsync(IFormFile formFile)
        {
            var user = await _userService.GetByIdAsync(User.GetUserId());
            var response = await _photoService.AddPhotoAsync(formFile);
            if (response == null)
            {
                return BadRequest(response.Error.Message);
            }

            var photo = new Photo
            {
                Url = response.SecureUrl.AbsoluteUri,
                PublicId = response.PublicId
            };

            if(user.Photos.Count == 0)
            {
                photo.IsMain = true;
            }
            user.Photos.Add(photo);

            if(await _userService.UpdateAsync(user))
            {
                return CreatedAtRoute("GetUser", new { id = user.Id }, _mapper.Map<PhotoDto>(photo));
            }
            return BadRequest("Upload failed");
        }
    }
}
