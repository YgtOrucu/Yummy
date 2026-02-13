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
                var client = _httpClientFactory.CreateClient("YummyClient");
                var responseMessage = await client.GetAsync("Features");
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

                var client = _httpClientFactory.CreateClient("YummyClient");
                var responsemessage = await client.PostAsJsonAsync("Features", createFeatureDto);

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
                var client = _httpClientFactory.CreateClient("YummyClient");
                var responsemessage = await client.DeleteAsync("Features?id=" + id);

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
                var client = _httpClientFactory.CreateClient("YummyClient");
                var responseMessage = await client.GetAsync("Features/GetFeatureById?id=" + id);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var values = await responseMessage.Content.ReadFromJsonAsync<UpdateFeatureDto>();
                    return View(values);
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

                var client = _httpClientFactory.CreateClient("YummyClient");
                var responseMessage = await client.PutAsJsonAsync("Features", updateFeatureDto);

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
