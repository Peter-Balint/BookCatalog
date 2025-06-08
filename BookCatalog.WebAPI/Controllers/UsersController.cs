using AutoMapper;
using BookCatalog.DataAccess.Models;
using BookCatalog.DataAccess.Services;
using BookCatalog.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalog.WebAPI.Controllers
{
    [ApiController]
    [Route("api/users/")]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _userService;
        private readonly IMapper _mapper;

        public UsersController(UsersService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] LoginDto loginDto)
        {
            try
            {

                var user = new User() { UserName = loginDto.UserName};

                await _userService.RegisterAsync(user, loginDto.Password!);

                var userResponseDto = _mapper.Map<UserDto>(user);

                return StatusCode(StatusCodes.Status201Created, userResponseDto);
            }
            catch (InvalidDataException e)
            {
                return Conflict(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var loggedInUser = await _userService.LoginAsync(loginDto.UserName, loginDto.Password);
                var userResponseDto = _mapper.Map<UserDto>(loggedInUser);

                return Ok(userResponseDto);
            }
            catch (AccessViolationException e)
            {
                return StatusCode(StatusCodes.Status403Forbidden, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        [Route("login-token")]
        public async Task<IActionResult> LoginToken([FromBody] LoginDto loginDto)
        {
            try
            {
                User loggedInUser = await _userService.LoginAsync(loginDto.UserName, loginDto.Password);
                UserDto userResponseDto = _mapper.Map<UserDto>(loggedInUser);

                // access token küldése header-ben
                Response.Headers.Append("X-Access-Token", $"{loggedInUser.UserName}|{loggedInUser.AccessToken}");
                Response.Headers.Append("Access-Control-Expose-Headers", "X-Access-Token"); // CORS

                return Ok(userResponseDto);
            }
            catch (AccessViolationException e)
            {
                return StatusCode(StatusCodes.Status403Forbidden, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        [Route("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _userService.LogoutAsync();

            return NoContent();
        }
    }
}
