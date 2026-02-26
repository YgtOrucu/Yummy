using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using Yummy.WebUI.Dtos.OpenAIDto;

namespace Yummy.WebUI.Controllers
{
    public class RecipeWithAIController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public string APIKEY => _configuration["APIKEY"];

        public RecipeWithAIController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult CreateRecipe()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecipe(string prompt)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("OpenAIClient");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", APIKEY);

                var RequestData = new
                {
                    model = "gpt-3.5-turbo",
                    messages = new[]
                    {
                            new
                            {
                               role = "system",
                               content = @"Sen 'Gurme Rehber' adında, esprili ve düzenli bir şefsin. Kullanıcının malzemelerine göre şu kurallara uyarak yanıt ver:
                               1. Başlık: Mutlaka ilgili bir emoji ile başla (Örn: 🧅 Ketçaplı Soğan Sote).
                               2. Özet: Tarifin altına tek cümlelik, iştah açıcı ve hafif esprili bir 'şef yorumu' ekle.
                               3. Süreler: Hazırlama ve Pişirme sürelerini ayrı ayrı belirt.
                               4. Malzemeler: Kullanıcının verdiği malzemeleri 'Senin Olanlar' başlığıyla listele. Eksik ama elzem olanları (yağ, su, tuz gibi) 'Mutfak Temelleri' başlığıyla ekle.
                               5. Hazırlanış: Her adımı kalın puntolu başlıklarla (Örn: 1. Soğanları hazırla:) yaz ve altına kısa, net talimatlar ekle.
                               6. Üslup: 'Sarımsak trip atarsa acılaşır' gibi samimi, mutfak jargonuna hakim ve eğlenceli bir dil kullan.
                               7. Görsellik: Önemli kelimeleri kalınlaştır, listeler ve emojiler kullanarak metni taranabilir yap."
                            },
                            new
                            {
                                role ="user",
                                content = prompt
                            }
                        },
                    temperature = 0.5
                };

                var JsonData = JsonConvert.SerializeObject(RequestData);
                var StringContent = new StringContent(JsonData, Encoding.UTF8, "application/json");

                var ResponseMessage = await client.PostAsync("chat/completions", StringContent);

                if (ResponseMessage.IsSuccessStatusCode)
                {
                    var ResponseString = await ResponseMessage.Content.ReadFromJsonAsync<OpenAIResponse>();
                    var value = ResponseString.Choice[0].Message.Content;
                    ViewBag.recipe = value;
                }
                else
                {
                    ViewBag.recipe = "Bir Hata Oluştu" + ResponseMessage.StatusCode;
                }

            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
            return View();
        }
    }
}
