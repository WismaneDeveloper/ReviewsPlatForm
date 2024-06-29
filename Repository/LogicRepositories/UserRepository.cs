using Entities.ContextDB;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;

namespace Repository.LogicRepositories
{
    public class UserRepository : ICUser
    {
        private readonly LibrosdbContext _context;

        public UserRepository(LibrosdbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateUser(Users user)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);

                    if (existingUser != null)
                    {
                        await transaction.RollbackAsync();
                        return false; // Indica que la operación no fue exitosa porque el usuario ya existe
                    }
                    else
                    {
                        _context.Users.Add(user);
                        await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos
                    }

                    await transaction.CommitAsync();
                    return true; // Indica éxito en la creación del usuario
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    await Console.Out.WriteLineAsync($"Error al crear un usuario: {ex.Message}");
                    return false; // Indica fallo en la creación del usuario debido a una excepción
                }
            }

        }

        public async Task<List<Users>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<Users?> GetUser(string Email, string PassWord)
        {
            return await _context.Users
                  .FirstOrDefaultAsync(u => u.Email == Email && u.Pass == PassWord);
        }





    }
}
