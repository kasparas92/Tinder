using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
        [HttpGet]
        [Route("GetByGender/{gender}")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetByGenderAsync(string gender)
        {
            var users = await _userService.GetByGenderAsync(gender);
            var response = _mapper.Map<IEnumerable<UserDto>>(users);
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
        [HttpDelete("delete-photos/{id}")]
        public async Task<ActionResult> DeletePhotoAsync(int id)
        {
            var user = await _userService.GetByIdAsync(User.GetUserId());
            var photo = user.Photos.FirstOrDefault(p => p.Id == id);
            if(photo == null)
            {
                return NotFound();
            }
            else if(photo.IsMain)
            {
                return BadRequest("You can't delete Main Photo");
            }
            else if(photo.PublicId != null)
            {
                var response = await _photoService.DeletePhotoAsync(photo.PublicId);
                if(response.Error != null)
                {
                    return BadRequest(response.Error.Message);
                }
            }

            user.Photos.Remove(photo);
            if (await _userService.UpdateAsync(user))
            {
                return Ok();
            }
            return BadRequest("Failed to Delete Photo!!!!!!!!!!!!!!");
        }
        [HttpPut("set-main-photo/{id}")]
        public async Task<ActionResult> SetMainPhoto(int id)
        {
            var user = await _userService.GetByIdAsync(User.GetUserId());
            var photo = user.Photos.FirstOrDefault(p => p.Id == id);
            if (photo.IsMain)
            {
                return BadRequest("This is already IsMain");
            }
            var currentMain = user.Photos.FirstOrDefault(x => x.IsMain);
            if(currentMain != null)
            {
                currentMain.IsMain = false;
            }
            photo.IsMain = true;
            if (await _userService.UpdateAsync(user))
            {
                return NoContent();
            }
            return BadRequest("Seting photo to main is failed!!!!");
        }
    }
}
