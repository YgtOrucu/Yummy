using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yummy.WebUI.Dtos.LoginDto;

namespace Yummy.WebUI.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public RegisterController(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
                return View(registerDto);

            var client = _httpClientFactory.CreateClient("YummyClient");

            var content = new MultipartFormDataContent();
            content.Add(new StringContent(registerDto.Name), "Name");
            content.Add(new StringContent(registerDto.Surname), "Surname");
            content.Add(new StringContent(registerDto.Email), "Email");
            content.Add(new StringContent(registerDto.UserName), "UserName");
            content.Add(new StringContent(registerDto.Password), "Password");

            if (registerDto.ImageFile != null)
            {
                var streamContent = new StreamContent(registerDto.ImageFile.OpenReadStream());
                content.Add(streamContent, "ImageFile", registerDto.ImageFile.FileName);
            }

            var response = await client.PostAsync("Auths/Register", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                var errorcontent = await response.Content.ReadFromJsonAsync<List<IdentityErrorResponse>>();
                foreach (var error in errorcontent)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(registerDto);
        }

        public class IdentityErrorResponse
        {
            public string Code { get; set; }
            public string Description { get; set; }
        }
    }
}
