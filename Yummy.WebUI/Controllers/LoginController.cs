using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Yummy.WebUI.Dtos.LoginDto;

namespace Yummy.WebUI.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return View(loginDto);


            var client = _httpClientFactory.CreateClient("YummyClient");
            var responseBody = await client.PostAsJsonAsync("Auths/Login", loginDto);

            if (responseBody.IsSuccessStatusCode)
            {
                var apiResponse = await responseBody.Content.ReadFromJsonAsync<ClaimProvider>();
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, apiResponse.Username),
                    new Claim(ClaimTypes.Email, apiResponse.Email),
                    new Claim("NameSurname", apiResponse.NameSurname)
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties { IsPersistent = true };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                return RedirectToAction("DashboardView", "Dashboard");
            }
            ModelState.AddModelError("", "Kullanıcı adı veya şifre yanlış.");
            return View(loginDto);
        }

        public async Task<IActionResult> Logout()
        {
            var client = _httpClientFactory.CreateClient("YummyClient");
            var response = await client.PostAsync("Auths/Logout", null);

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Login", "Login");

            return View();

        }
        public class ClaimProvider
        {
            public string Message { get; set; }
            public string Username { get; set; }
            public string NameSurname { get; set; }
            public string Email { get; set; }
        }
    }
}
