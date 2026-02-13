using Microsoft.AspNetCore.Mvc;
using Yummy.WebUI.Dtos.ChefDto;

namespace Yummy.WebUI.Controllers
{
    public class ChefController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ChefController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> ChefList()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("YummyClient");
                var responseMessage = await client.GetAsync("Chefs");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultChefDto>>();
                    return View(values);
                }
                return View();
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult ChefCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChefCreate(CreateChefDto createChefDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(createChefDto);
                }

                if (createChefDto.ImageFile != null)
                {
                    var resource = Directory.GetCurrentDirectory();
                    var extension = Path.GetExtension(createChefDto.ImageFile.FileName);
                    var imageName = Guid.NewGuid() + extension;

                    var uploadPath = Path.Combine(resource, "wwwroot", "images", "ChefImage");
                    var saveLocation = Path.Combine(uploadPath, imageName);
                    using var stream = new FileStream(saveLocation, FileMode.Create);
                    await createChefDto.ImageFile.CopyToAsync(stream);
                    createChefDto.ImageUrl = "/images/ChefImage/" + imageName;
                }

                var client = _httpClientFactory.CreateClient("YummyClient");
                var responseMessage = await client.PostAsJsonAsync("Chefs", createChefDto);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("ChefList");
                }
                else
                {
                    return View(createChefDto);
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public async Task<IActionResult> ChefDelete(int id)
        {
            try
            {
                var resource = Directory.GetCurrentDirectory();
                var oldImage = await GetOldImagControlSelectChef(id);
                if (!string.IsNullOrEmpty(oldImage.ImageUrl))
                {
                    var oldImagePath = Path.Combine(
                        resource,
                        "wwwroot",
                        oldImage.ImageUrl.TrimStart('/')
                    );

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                var client = _httpClientFactory.CreateClient("YummyClient");
                var responseMessage = await client.DeleteAsync("Chefs?id=" + id);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("ChefList");
                }
                return View();
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ChefUpdate(int id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("YummyClient");
                var responseMessage = await client.GetAsync("Chefs/GetChefById?id=" + id);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var values = await responseMessage.Content.ReadFromJsonAsync<UpdateChefDto>();
                    return View(values);
                }
                return View();
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ChefUpdate(UpdateChefDto updateChefDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(updateChefDto);
                }

                if (updateChefDto.ImageFile != null)
                {
                    var resource = Directory.GetCurrentDirectory();
                    var extension = Path.GetExtension(updateChefDto.ImageFile.FileName);
                    var imageName = Guid.NewGuid() + extension;

                    var uploadPath = Path.Combine(resource, "wwwroot", "images", "ChefImage");
                    var saveLocation = Path.Combine(uploadPath, imageName);

                    var oldImage = await GetOldImagControlSelectChef(updateChefDto.ChefId);
                    if (!string.IsNullOrEmpty(oldImage.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(
                            resource,
                            "wwwroot",
                            oldImage.ImageUrl.TrimStart('/')
                        );

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using var stream = new FileStream(saveLocation, FileMode.Create);
                    await updateChefDto.ImageFile.CopyToAsync(stream);
                    updateChefDto.ImageUrl = "/images/ChefImage/" + imageName;
                }
                var client = _httpClientFactory.CreateClient("YummyClient");
                var responseMessage = await client.PutAsJsonAsync("Chefs", updateChefDto);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("ChefList");
                }
                return View(updateChefDto);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        private async Task<GetChefByIdDto> GetOldImagControlSelectChef(int ChefId)
        {
            var client = _httpClientFactory.CreateClient("YummyClient");
            var responseMessage = await client.GetAsync("Chefs/GetChefById?id=" + ChefId);

            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<GetChefByIdDto>();
                return values;
            }
            return new GetChefByIdDto();
        }
    }
}
