using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tinder.API.Services.Interfaces;
using Tinder.ServiceModel.Dtos.Requests;
using Tinder.ServiceModel.Dtos.Responses;

namespace Tinder.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        public AccountController(IAccountService accountService, IMapper mapper, ITokenService tokenService)
        {
            _accountService = accountService;
            _mapper = mapper;
            _tokenService = tokenService;
        }
        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto register)
        {
            if (await _accountService.IsUserExist(register.Name))
            {
                return BadRequest("UserName already exists!!!");
            }
            var user = await _accountService.Register(register.Name, register.Password, register.Gender, register.Country);
            var userDetails = _mapper.Map<UserDetailsDto>(user);
            return Ok(userDetails);
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginRequestDto login)
        {
            if (!await _accountService.IsUserExist(login.Name))
            {
                return Unauthorized("Invalid User!!");
            }
            var user = await _accountService.Login(login.Name, login.Password);
            if(user == null)
            {
                return Unauthorized("Invalid Password!!");
            }
            var response = _mapper.Map<LoginDto>(user);
            response.Token = _tokenService.CreateToken(user);
            return Ok(response);
        }
    }
}
