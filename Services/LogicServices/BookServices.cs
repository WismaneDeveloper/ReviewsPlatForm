using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;
using Repository.LogicRepositories;
using Services.ContractServices;
using Services.DataTransferObject.Books;
using Services.HandlerException;

namespace Services.LogicServices
{
    /// <summary>
    /// Clase que implementa la lógica de negocio para la gestión de libros.
    /// </summary>
    public class BookServices : IBookServices
    {
        private readonly ICBook _iBook;

        /// <summary>
        /// Constructor de la clase BookServices.
        /// </summary>
        /// <param name="iBook">Interfaz de repositorio de libros para acceder a los datos.</param>
        public BookServices(ICBook iBook)
        {
            _iBook = iBook;
        }

        /// <summary>
        /// Crea un nuevo libro en la base de datos.
        /// </summary>
        /// <param name="bookDto">DTO con la información del libro a crear.</param>
        /// <returns>RequestResult con el resultado de la operación.</returns>
        public async Task<RequestResult<bool>> CreateBookDTO(CreateBookDTO bookDto)
        {
            try
            {
                if (bookDto is null)
                    return RequestResult<bool>.CreateUnsuccessful("Se requieren datos para completar el registro.");

                Book book = new Book
                {
                    Title = bookDto.Title,
                    Author = bookDto.Author,
                    Category = bookDto.Category,
                    Descriptions = bookDto.Descriptions,
                    Rute = bookDto.Rute,
                    Img = bookDto.Img
                };

                bool CreateBookResult = await _iBook.CreateBook(book);

                if (CreateBookResult)
                {
                    return RequestResult<bool>.CreateSuccessful(CreateBookResult);
                }
                else
                {
                    return RequestResult<bool>.CreateUnsuccessful("No se pudo crear el libro. " +
                        "Asegúrese de que no exista o de que está ingresando correctamente los datos.");
                }
            }
            catch (DbUpdateException dbEx)
            {
                var innerExceptionMessage = dbEx.InnerException != null ? dbEx.InnerException.Message : dbEx.Message;
                return RequestResult<bool>.CreateError($"Error al intentar crear un libro: {innerExceptionMessage}");
            }
            catch (Exception ex)
            {
                return RequestResult<bool>.CreateError($"Error inesperado al intentar crear un libro: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene todos los libros registrados en la base de datos.
        /// </summary>
        /// <returns>RequestResult con la lista de libros obtenidos.</returns>
        public async Task<RequestResult<List<BookDTO>>> GetAllBooksDTO()
        {
            try
            {
                var Listbooks = await _iBook.GetAllBooks();

                if (Listbooks.Count == 0 || Listbooks is null)
                {
                    return RequestResult<List<BookDTO>>.CreateUnsuccessful("No hay libros registrados por el momento.");
                }

                var books = Listbooks.Select(book => new BookDTO
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    Category = book.Category,
                    Descriptions = book.Descriptions,
                    Rute = book.Rute,
                    Img = book.Img,
                }).ToList();

                return RequestResult<List<BookDTO>>.CreateSuccessful(books);
            }
            catch (Exception ex)
            {
                return RequestResult<List<BookDTO>>.CreateError(ex);
            }
        }

        /// <summary>
        /// Busca libros en la base de datos según una referencia.
        /// </summary>
        /// <param name="referenceBook">Referencia para buscar libros (título, autor o categoría).</param>
        /// <returns>RequestResult con la lista de libros encontrados.</returns>
        public async Task<RequestResult<List<BookDTO>>> GetSearchedBooks(string referenceBook)
        {
            try
            {
                var listBook = await _iBook.SearchBooks(referenceBook);

                var books = listBook.Select(book => new BookDTO
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    Category = book.Category,
                    Descriptions = book.Descriptions,
                    Rute = book.Rute,
                    Img = book.Img,
                }).Where(book =>
                book.Title.Contains(referenceBook, StringComparison.OrdinalIgnoreCase) ||
                book.Author.Contains(referenceBook, StringComparison.OrdinalIgnoreCase) ||
                book.Category.Contains(referenceBook, StringComparison.OrdinalIgnoreCase))
                .ToList();

                if (books is null || books.Count == 0)
                    return RequestResult<List<BookDTO>>.CreateUnsuccessful($"No existe ningún libro con la referencia de búsqueda: {referenceBook}");

                return RequestResult<List<BookDTO>>.CreateSuccessful(books);
            }
            catch (Exception ex)
            {
                return RequestResult<List<BookDTO>>.CreateError(ex);
            }
        } 

    }
}
