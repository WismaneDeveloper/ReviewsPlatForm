using Microsoft.AspNetCore.Mvc;

namespace ReviewPlatFormWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient _httpClient;

        public UserController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7100/api");
        }   
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult ForgotPassWord()
        {
            return View();
        }
        public async Task< IActionResult> RegisterUser()
        {
            return View();
        }



    }
}
