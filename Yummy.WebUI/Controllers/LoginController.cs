using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Yummy.WebAPI.Dtos.ResetPasswordDto;
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
        public IActionResult EmailVerification()
        {
            var email = TempData["UserEmail"]?.ToString();
            if (string.IsNullOrEmpty(email)) return RedirectToAction("Login");
            TempData.Keep("UserEmail");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EmailVerification(int confirmCode)
        {
            var email = TempData["UserEmail"]?.ToString();
            var client = _httpClientFactory.CreateClient("YummyClient");

            var response = await client.PostAsJsonAsync("Auths/VerifyCode", new { Email = email, Code = confirmCode });

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadFromJsonAsync<ClaimProvider>();

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, apiResponse.Username),
                    new Claim(ClaimTypes.Email, apiResponse.Email),
                    new Claim("NameSurname", apiResponse.NameSurname)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties { IsPersistent = true };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), authProperties);

                return RedirectToAction("DashboardView", "Dashboard");
            }

            ModelState.AddModelError("", "Girdiğiniz kod hatalı.");
            TempData.Keep("UserEmail");
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid) return View(loginDto);

            var client = _httpClientFactory.CreateClient("YummyClient");
            var responseBody = await client.PostAsJsonAsync("Auths/Login", loginDto);

            if (responseBody.IsSuccessStatusCode)
            {
                var apiResponse = await responseBody.Content.ReadFromJsonAsync<ClaimProvider>(); 

                if (apiResponse.RequiresConfirmation == true)
                {
                    TempData["UserEmail"] = loginDto.Email;
                    return RedirectToAction("EmailVerification");
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, apiResponse.Username),
                    new Claim(ClaimTypes.Email, apiResponse.Email),
                    new Claim("NameSurname", apiResponse.NameSurname)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties { IsPersistent = true };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), authProperties);

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
            public string Username { get; set; }
            public string NameSurname { get; set; }
            public string Email { get; set; }
            public bool RequiresConfirmation { get; set; }
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var client = _httpClientFactory.CreateClient("YummyClient");

            var response = await client.PostAsJsonAsync("Auths/ForgotPassword", email);

            if (response.IsSuccessStatusCode)
            {
                TempData["ResetEmail"] = email;
                return RedirectToAction("ResetPassword");
            }

            ModelState.AddModelError("", "E-posta gönderilemedi. Lütfen adresi kontrol edin.");
            return View();
        }


        [HttpGet]
        public IActionResult ResetPassword()
        {
            var email = TempData["ResetEmail"]?.ToString();

            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("ForgotPassword");
            }
            TempData.Keep("ResetEmail");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
        {
            if (resetPassword.NewPassword != resetPassword.ConfirmPassword)
            {
                ModelState.AddModelError("", "Şifreler birbiriyle uyuşmuyor.");
                TempData.Keep("ResetEmail");
                return View(resetPassword);
            }
            var client = _httpClientFactory.CreateClient("YummyClient");
            var response = await client.PostAsJsonAsync("Auths/ResetPassword", resetPassword);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Şifreniz başarıyla güncellendi. Yeni şifrenizle giriş yapabilirsiniz.";
                return RedirectToAction("Login");
            }
            var errorMessage = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", errorMessage ?? "Şifre sıfırlanamadı, lütfen kodu kontrol edin.");
            TempData.Keep("ResetEmail");
            return View(resetPassword);
        }
    }
}
