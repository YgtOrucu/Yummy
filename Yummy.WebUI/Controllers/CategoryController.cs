using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Yummy.WebUI.Dtos.CategoryDto;

namespace Yummy.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> CategoryList()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var responseMessage = await client.GetAsync("https://localhost:7287/api/Categories");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultCategoryDto>>();
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
        public IActionResult CategoryCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CategoryCreate(CreateCategoryDto createCategoryDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(createCategoryDto);

                var client = _httpClientFactory.CreateClient();
                var JsonData = JsonConvert.SerializeObject(createCategoryDto);
                var StringContent = new StringContent(JsonData, Encoding.UTF8, "application/json");
                var responsemessage = await client.PostAsync("https://localhost:7287/api/Categories", StringContent);

                if (responsemessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("CategoryList");
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
            return View();
        }

        public async Task<IActionResult> CategoryDelete(int id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var responsemessage = await client.DeleteAsync("https://localhost:7287/api/Categories?id=" + id);

                if (responsemessage.IsSuccessStatusCode)
                    return RedirectToAction("CategoryList");

            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CategoryUpdate(int id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var responseMessage = await client.GetAsync("https://localhost:7287/api/Categories/GetCategoryById?id=" + id);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var valuess = await responseMessage.Content.ReadFromJsonAsync<GetCategoryByIdDto>();
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
        public async Task<IActionResult> CategoryUpdate(UpdateCategoryDto updateCategoryDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(updateCategoryDto);

                var client = _httpClientFactory.CreateClient();
                var JsonData = JsonConvert.SerializeObject(updateCategoryDto);
                var stringContent = new StringContent(JsonData, Encoding.UTF8, "application/json");
                var responseMessage = await client.PutAsync("https://localhost:7287/api/Categories", stringContent);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("CategoryList");
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
