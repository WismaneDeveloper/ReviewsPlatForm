using Entities.Models;
using Services.DataTransferObject.Users;
using Services.HandlerException;

namespace Services.ContractServices
{
    public interface IUserService
    {

        Task<RequestResult<bool>> CreateUserRequest(UserCreateDTO userDTO);

        Task<RequestResult<UserDTO>> Login(string Email, string PassWord);

        Task<RequestResult<List<UserDTO>>> GetUsersDTO();



    }
}
