using Microsoft.AspNetCore.Mvc;
using Services;
using Services.ContractServices;
using Services.DataTransferObject.Users;
using Services.HandlerException;

namespace PlatFormWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("CrearUsuario")]
        public async Task<ActionResult<RequestResult<bool>>> CreateUser([FromBody] UserCreateDTO user)
        {
            return Ok(await _userService.CreateUserRequest(user));
        }
        [HttpGet]
        [Route("ObtenerUsuarios")]
        public async Task<ActionResult<RequestResult<List<UserDTO>>>> GetUsers()
        {
            return Ok(await _userService.GetUsersDTO());
        } 
        
        [HttpPost]
        [Route("ObtenerUsuario")]
        public async Task<ActionResult<RequestResult<UserDTO>>> GetUser(string Email, string Pass)
        {
            string PassWord = EncryptPassword.ConvertTOSHA256(Pass);
            return Ok(await _userService.Login(Email, PassWord));
        }



    }
}
