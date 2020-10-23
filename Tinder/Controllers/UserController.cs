using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tinder.API.Extensions;
using Tinder.API.Services.Interfaces;
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
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllAsync()
        {
            var users = await _userService.GetAllAsync();
            var response = _mapper.Map<IEnumerable<UserDto>>(users);
            return Ok(response);
        }
        [HttpGet("{id}")]
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

    }
}
