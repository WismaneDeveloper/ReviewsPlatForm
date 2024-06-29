using Entities.Models;

namespace Repository.Contracts
{
    public interface ICUser
    {
        Task<bool> CreateUser(Users user);

        Task<Users?> GetUser(string Email, string PassWord);

        Task<List<Users>> GetAllUsers(); 



    }
}
