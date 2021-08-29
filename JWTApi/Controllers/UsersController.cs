using System.Threading.Tasks;
using AutoMapper;
using EBET.Data;
using EBET.Dtos;
using EBET.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EBET.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserservice _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserservice userService, IMapper mapper) 
        {
            _userService = userService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateModel model)
        {
            var user = await _userService.Authenticate(model.Username, model.Password);

            if(user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(_mapper.Map<UserForLoginDto>(user));
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserForRegisterDto userForRegisterDto)
        {

            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

            if(await _userService.UserExist(userForRegisterDto.Username))
                ModelState.AddModelError("Username", "Username already exist");


            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var userToCreate = new User
            {
                Name = userForRegisterDto.Username,
                Email = userForRegisterDto.Email,
                Phone = userForRegisterDto.Phone,
                Role = userForRegisterDto.Role
            };

            var createUser = await _userService.Register(userToCreate, userForRegisterDto.Password);

            return StatusCode(201);
        }

        //[HttpGet]
        // public IActionResult GetAll()
        // {
        //     var users = _userService.GetAll();
        //     return Ok(users);
        // }
    }
}
