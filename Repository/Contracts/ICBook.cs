using Entities.Models;

namespace Repository.Contracts
{
    public interface ICBook
    {
        public Task<List<Book>> GetAllBooks();

        Task<bool> CreateBook(Book book);

        Task<List<Book>> SearchBooks(string bookReference);



    }
}
