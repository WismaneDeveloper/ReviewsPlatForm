using Microsoft.AspNetCore.Mvc;
using Services.ContractServices;
using Services.DataTransferObject.Books;
using Services.HandlerException;

namespace PlatFormWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        private readonly IBookServices _bookService;
        public BookController(IBookServices bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        [Route("ObtenerLibros")]

        public async Task<ActionResult<List<BookDTO>>> GetBooksDto()
        {
            return Ok(await _bookService.GetAllBooksDTO());
        }


        [HttpPost]
        [Route("CrearLibro")]

        public async Task<ActionResult<RequestResult<bool>>> CreateBookDto(CreateBookDTO bookDTO)
        {
            return Ok(await _bookService.CreateBookDTO(bookDTO));
        }

        [HttpGet]
        [Route("ObtenerLibroBuscado")]

        public async Task<ActionResult<List<BookDTO>>> GetSearchsBooks(string Reference)
        {
            return Ok(await _bookService.GetSearchedBooks(Reference));
        }






    }
}
