using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Yummy.WebUI.Dtos.FeatureDto;

namespace Yummy.WebUI.Controllers
{
    public class FeatureController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FeatureController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> FeatureList()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var responseMessage = await client.GetAsync("https://localhost:7287/api/Features");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultFeatureDto>>();
                    return View(values);
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
            return View();
        }

        [HttpGet]
        public IActionResult FeatureCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FeatureCreate(CreateFeatureDto createFeatureDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(createFeatureDto);

                var client = _httpClientFactory.CreateClient();
                var responsemessage = await client.PostAsJsonAsync("https://localhost:7287/api/Features", createFeatureDto);

                if (responsemessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("FeatureList");
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
            return View(createFeatureDto);
        }

        public async Task<IActionResult> FeatureDelete(int id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var responsemessage = await client.DeleteAsync("https://localhost:7287/api/Features?id=" + id);

                if (responsemessage.IsSuccessStatusCode)
                    return RedirectToAction("FeatureList");

            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> FeatureUpdate(int id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var responseMessage = await client.GetAsync("https://localhost:7287/api/Features/GetFeatureById?id=" + id);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var valuess = await responseMessage.Content.ReadFromJsonAsync<UpdateFeatureDto>();
                    return View(valuess);
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> FeatureUpdate(UpdateFeatureDto updateFeatureDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(updateFeatureDto);

                var client = _httpClientFactory.CreateClient();
                var responseMessage = await client.PutAsJsonAsync("https://localhost:7287/api/Features", updateFeatureDto);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("FeatureList");
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
            return View(updateFeatureDto);
        }
    }
}
