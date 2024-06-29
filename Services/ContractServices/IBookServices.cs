using Microsoft.AspNetCore.Http;
using Services.DataTransferObject.Books;
using Services.HandlerException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ContractServices
{
    public interface IBookServices
     {
        Task<RequestResult<List<BookDTO>>> GetAllBooksDTO();
        Task<RequestResult<List<BookDTO>>> GetSearchedBooks(string referenceBook);
        Task<RequestResult<bool>> CreateBookDTO(CreateBookDTO bookDto);




    }
}
