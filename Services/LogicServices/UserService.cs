using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;
using Services.ContractServices;
using Services.DataTransferObject.Users;
using Services.HandlerException;

namespace Services.LogicServices
{
    public class UserService : IUserService
    {
        private readonly ICUser _user;

        public UserService(ICUser user)
        {
            _user = user;
        }

        public async Task<RequestResult<bool>> CreateUserRequest(UserCreateDTO userDTO)
        {
            try
            {
                if (userDTO == null)
                {
                    return RequestResult<bool>.CreateUnsuccessful("Se requieren algunos datos para completar el registro");
                }

                if (string.IsNullOrWhiteSpace(userDTO.Pass))
                {
                    return RequestResult<bool>.CreateUnsuccessful("La contraseña es obligatoria.");
                }


                string encryptedPassword = EncryptPassword.ConvertTOSHA256(userDTO.Pass);

                Users user = new Users
                {
                    UserName = userDTO.UserName,
                    Pass = encryptedPassword,
                    Email = userDTO.Email,
                    Rute = userDTO.Rute,
                    Img = userDTO.Img
                };

                bool createUserResult = await _user.CreateUser(user);

                if (createUserResult)
                {
                    return RequestResult<bool>.CreateSuccessful(true);
                }
                else
                {
                    return RequestResult<bool>.CreateUnsuccessful("No se pudo crear el usuario. Verifica los datos ingresados.");
                }
            }
            catch (DbUpdateException dbEx)
            {
                // Registra los detalles de la excepción interna
                var innerExceptionMessage = dbEx.InnerException != null ? dbEx.InnerException.Message : dbEx.Message;
                return RequestResult<bool>.CreateError($"Error al intentar crear el usuario: {innerExceptionMessage}");
            }
            catch (Exception ex)
            {
                return RequestResult<bool>.CreateError($"Error inesperado al intentar crear el usuario: {ex.Message}");
            }
        }

        public async Task<RequestResult<List<UserDTO>>> GetUsersDTO()
        {
            try
            {
                var listUser = await _user.GetAllUsers();

                if (listUser is null)
               return RequestResult<List<UserDTO>>.CreateUnsuccessful("No hay usuarios registrados por el momento.");
                var users = listUser.Select(U => new UserDTO { 
                    Id = U.Id,
                    UserName = U.UserName,
                    UserEmail = U.Email,

                }).ToList();

                return RequestResult<List<UserDTO>>.CreateSuccessful(users);
            }
            catch (Exception ex )
            {

                return RequestResult<List<UserDTO>>.CreateError($"Ha ocurrido un error inesperado:{ex.Message}");
              
            }
        }

        public async Task<RequestResult<UserDTO>> Login(string Email, string password)
        {
            try
            {
                var user = await _user.GetUser(Email, password);

                if (user == null)
                {
                    return RequestResult<UserDTO>.CreateUnsuccessful("Usuario no encontrado o credenciales inválidas.");
                }

                
                

                var userDTO = new UserDTO
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    UserEmail = user.Email,
                };

                return RequestResult<UserDTO>.CreateSuccessful(userDTO);
            }
            catch (Exception ex)
            {
                return RequestResult<UserDTO>.CreateError($"Error al intentar iniciar sesión: {ex.Message}");
            }
        }

    }
}
