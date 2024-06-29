using Entities.ContextDB;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;

namespace Repository.LogicRepositories
{
    /// <summary>
    /// Implementación del repositorio de libros que maneja las operaciones de acceso a datos para la entidad Book.
    /// </summary>
    public class BookRepository : ICBook
    {
        private readonly LibrosdbContext _context;

        /// <summary>
        /// Constructor que inyecta el contexto de la base de datos.
        /// </summary>
        /// <param name="context">El contexto de la base de datos.</param>
        public BookRepository(LibrosdbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Crea un nuevo libro en la base de datos.
        /// </summary>
        /// <param name="book">El objeto Book que se va a crear.</param>
        /// <returns>Devuelve true si el libro se creó exitosamente, de lo contrario, false.</returns>
        public async Task<bool> CreateBook(Book book)
        {
            // Inicia una transacción de base de datos.
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Verifica si el libro ya existe en la base de datos.
                    var existingBook = await _context.Book.FirstOrDefaultAsync(b => b.Id == book.Id);
                    if (existingBook != null)
                    {
                        // Si el libro ya existe, revierte la transacción y devuelve false.
                        await transaction.RollbackAsync();
                        return false; // Indica que el libro ya existe
                    }
                    else
                    {
                        // Si el libro no existe, lo agrega a la base de datos y guarda los cambios.
                        _context.Book.Add(book);
                        await _context.SaveChangesAsync();
                    }

                    // Confirma la transacción y devuelve true indicando éxito.
                    await transaction.CommitAsync();
                    return true; // Indica que se creó el libro exitosamente
                }
                catch (Exception ex)
                {
                    // En caso de error, revierte la transacción y registra el error.
                    await transaction.RollbackAsync();
                    Console.WriteLine($"Error al crear el libro: {ex.Message}");
                    return false;
                }
            }
        }

        /// <summary>
        /// Recupera todos los libros de la base de datos.
        /// </summary>
        /// <returns>Una lista de objetos Book.</returns>
        public async Task<List<Book>> GetAllBooks()
        {
            return await _context.Book.ToListAsync();
        }

        /// <summary>
        /// Busca libros en la base de datos que coincidan con la referencia proporcionada.
        /// </summary>
        /// <param name="bookReference">La referencia del libro para buscar.</param>
        /// <returns>Una lista de objetos Book que coinciden con la referencia.</returns>
        public async Task<List<Book>> SearchBooks(string bookReference)
        {
            // Nota: Esta implementación busca todos los libros, debería ser actualizada para buscar por referencia.
            return await _context.Book
                .Where(b => b.Title.Contains(bookReference) || b.Author.Contains(bookReference))
                .ToListAsync();
        }
    }


}
