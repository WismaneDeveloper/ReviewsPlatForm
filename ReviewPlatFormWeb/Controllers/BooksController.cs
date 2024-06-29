using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ReviewPlatFormWeb.Models;
using Services.HandlerException;
using System.Net.Http;
using System.Threading.Tasks;

namespace ReviewPlatFormWeb.Controllers
{
    public class BooksController : Controller
    {
        private readonly HttpClient _httpClient;

        public BooksController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7100/api");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            TransferenceModel transference;
            HttpResponseMessage httpResponse = await _httpClient.GetAsync("api/Book/ObtenerLibros");

            LogResponse("ObtenerLibros", httpResponse);

            if (!httpResponse.IsSuccessStatusCode)
            {
                transference = new TransferenceModel();
                return View(transference);
            }

            var content = await httpResponse.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<Book>>>(content);

            if (apiResponse.ResultType == RequestResultType.Successful)
            {
                transference = new TransferenceModel
                {
                    GetBooks = apiResponse.Result
                };
            }
            else
            {
                transference = new TransferenceModel();
                // Maneja errores si es necesario, por ejemplo, mostrando mensajes en la vista
            }

            return View(transference);
        }




        private void LogResponse(string API, HttpResponseMessage message)
        {
            if (!message.IsSuccessStatusCode)
            {
                Console.WriteLine($"La solicitud a: {API}, falló con el código de estado: {message.StatusCode}, y razón {message.ReasonPhrase}");
            }
            else
            {
                Console.WriteLine($"La solicitud a: {API}, Fue exitosa con el código de estado: {message.StatusCode}");
            }
        } 



    }
}
