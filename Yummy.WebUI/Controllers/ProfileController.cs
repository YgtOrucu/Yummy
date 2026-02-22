using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Yummy.WebUI.Dtos.ProfileDto;

namespace Yummy.WebUI.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProfileController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var username = User.Identity.Name;
            var client = _httpClientFactory.CreateClient("YummyClient");
            var response = await client.GetAsync($"Auths/GetProfile?username={username}");

            if (response.IsSuccessStatusCode)
            {
                var value = await response.Content.ReadFromJsonAsync<UpdateProfileDto>();
                return View(value);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UpdateProfileDto updateProfileDto)
        {
            var client = _httpClientFactory.CreateClient("YummyClient");

            using var content = new MultipartFormDataContent();
            content.Add(new StringContent(updateProfileDto.Username), "Username");
            content.Add(new StringContent(updateProfileDto.Name), "Name");
            content.Add(new StringContent(updateProfileDto.Surname), "Surname");
            content.Add(new StringContent(updateProfileDto.Email), "Email");

            if (updateProfileDto.Password != null)
                content.Add(new StringContent(updateProfileDto.Password), "Password");

            if (updateProfileDto.ImageFile != null)
            {
                var fileContent = new StreamContent(updateProfileDto.ImageFile.OpenReadStream());
                content.Add(fileContent, "ImageFile", updateProfileDto.ImageFile.FileName);
            }

            var response = await client.PutAsync("Auths/UpdateProfile", content);

            if (response.IsSuccessStatusCode)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                TempData["EditProfileMessage"] = "Profil bilgileriniz güncellendi. Lütfen yeni bilgilerinizle tekrar giriş yapın.";

                return RedirectToAction("Login", "Login");
            }

            return View(updateProfileDto);
        }
    }
}
