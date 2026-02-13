using Microsoft.AspNetCore.Mvc;
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
                var client = _httpClientFactory.CreateClient("YummyClient");
                var responseMessage = await client.GetAsync("Categories");
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

                var client = _httpClientFactory.CreateClient("YummyClient");
                var responsemessage = await client.PostAsJsonAsync("Categories", createCategoryDto);

                if (responsemessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("CategoryList");
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
            return View(createCategoryDto);
        }

        public async Task<IActionResult> CategoryDelete(int id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("YummyClient");
                var responsemessage = await client.DeleteAsync("Categories?id=" + id);

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
                var client = _httpClientFactory.CreateClient("YummyClient");
                var responseMessage = await client.GetAsync("Categories/GetCategoryById?id=" + id);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var valuess = await responseMessage.Content.ReadFromJsonAsync<UpdateCategoryDto>();
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

                var client = _httpClientFactory.CreateClient("YummyClient");
                var responseMessage = await client.PutAsJsonAsync("Categories", updateCategoryDto);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("CategoryList");
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
            return View(updateCategoryDto);
        }
    }
}
