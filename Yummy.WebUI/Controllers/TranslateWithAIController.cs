using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using Yummy.WebUI.Dtos.OpenAIDto;

namespace Yummy.WebUI.Controllers
{
    public class TranslateWithAIController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public string APIKEY => _configuration["APIKEY"];

        public TranslateWithAIController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult CreateTranslate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateTranslate(string prompt, string targetLanguage)
        {

            var client = _httpClientFactory.CreateClient("OpenAIClient");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", APIKEY);

            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new
                    {
                        role = "system",
                        content = $"Sen profesyonel bir çevirmensin. Sadece kullanıcıdan gelen mesajı {targetLanguage} diline çevir, giriş veya çıkış cümleleri kurma.Uzun metinleri çevirirken paragraf yapısını bozma ve akıcılığı koru."
                    },
                    new
                    {
                        role = "user",
                        content = prompt
                    }
                },
                temperature = 0.2
            };

            var json = JsonConvert.SerializeObject(requestBody);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync("chat/completions", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                var requestvalue = await responseMessage.Content.ReadFromJsonAsync<OpenAIResponse>();
                var value = requestvalue.Choice[0].Message.Content;
                ViewBag.translatedText = value;
            }
            else
            {
                ViewBag.translatedText = $"Bir Hata Oluştu : {responseMessage.StatusCode}";
            }
            return View();
        }
    }
}
