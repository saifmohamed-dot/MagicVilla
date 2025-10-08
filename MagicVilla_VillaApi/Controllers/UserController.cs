using AutoMapper;
using MagicVilla_VillaApi.Dto;
using MagicVilla_VillaApi.Models;
using MagicVilla_VillaApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly IUserRepository _userRepository;
        readonly IMapper _mapper;
        APIResponse _response;
        public UserController(IUserRepository userRepository , IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _response = new APIResponse();
        }
        // List All User
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetUsers()
        {
            List<UserDto> localUsers = _mapper.Map<List<UserDto>>(await _userRepository.GetAllUsersAsync());
            _response.PopulateOnSuccess(System.Net.HttpStatusCode.OK, localUsers);
            return Ok(_response);
            
        }
        // authenticate the user with jwt token 
        [HttpPost("login")]
        [ProducesResponseType(200)] // success login OK
        [ProducesResponseType(401)] // failed login unauthorized
        public async Task<ActionResult<APIResponse>> AuthenticateUser(LoginRequestDto userdto)
        {
            LoginResponseDto resp = await _userRepository.LoginUserAsync(userdto);
            if (resp.IsSuccess)
            {
                _response.PopulateOnSuccess(System.Net.HttpStatusCode.OK, resp);
                return Ok(_response);
            }
            else
            {
                _response.PopulateOnFail(System.Net.HttpStatusCode.Unauthorized , resp.Errors);
                return Unauthorized(_response);
            }
        }
        // register the user
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)] // success registration 
        [ProducesResponseType(StatusCodes.Status409Conflict)] // duplicated usernames 
        public async Task<ActionResult<APIResponse>> CreateUser(UserRegistrationRequestDto registerDto)
        {
            LocalUser resp = await _userRepository.RegisterUserAsync(registerDto);
            if (resp == null)
            {
                _response.PopulateOnFail(System.Net.HttpStatusCode.Conflict, errors: new List<string> { "This UserName Already Taken!" });
                return Conflict(_response);
            }
            else
            {
                _response.PopulateOnSuccess(System.Net.HttpStatusCode.OK, _mapper.Map<UserDto>(resp));
                return Ok(_response);
            }
        }
    }
}
