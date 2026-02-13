using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Yummy.WebUI.Dtos.HeroDto;

namespace Yummy.WebUI.Controllers
{
    public class HeroController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HeroController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> HeroList()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var responseMessage = await client.GetAsync("https://localhost:7287/api/Heroes");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultHeroDto>>();
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
        public IActionResult HeroCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> HeroCreate(CreateHeroDto createHeroDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(createHeroDto);

                if (createHeroDto.ImageFile != null)
                {
                    var resource = Directory.GetCurrentDirectory();
                    var extension = Path.GetExtension(createHeroDto.ImageFile.FileName);
                    var imageName = Guid.NewGuid() + extension;

                    var uploadPath = Path.Combine(resource, "wwwroot", "images", "ProductImage");
                    var saveLocation = Path.Combine(uploadPath, imageName);
                    using var stream = new FileStream(saveLocation, FileMode.Create);
                    await createHeroDto.ImageFile.CopyToAsync(stream);
                    createHeroDto.ImageUrl = "/images/ProductImage/" + imageName;
                }

                var client = _httpClientFactory.CreateClient();
                var JsonData = JsonConvert.SerializeObject(createHeroDto);
                var stringContent = new StringContent(JsonData, Encoding.UTF8, "application/json");
                var responseMessage = await client.PostAsync("https://localhost:7287/api/Heroes", stringContent);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("HeroList");
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
            return View(createHeroDto);
        }

        public async Task<IActionResult> HeroDelete(int id)
        {
            try
            {
                var resource = Directory.GetCurrentDirectory();
                var oldImage = await GetOldImagControlSelectHero(id);
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
                var client = _httpClientFactory.CreateClient();
                var responseMessage = await client.DeleteAsync("https://localhost:7287/api/Heroes?id=" + id);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("HeroList");
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> HeroUpdate(int id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var responseMessage = await client.GetAsync("https://localhost:7287/api/Heroes/GetHeroById?id=" + id);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var values = await responseMessage.Content.ReadFromJsonAsync<GetHeroByIdDto>();
                    return View(values);
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
                throw;
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> HeroUpdate(UpdateHeroDto updateHeroDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(updateHeroDto);

                if (updateHeroDto.ImageFile != null)
                {
                    var resource = Directory.GetCurrentDirectory();
                    var extension = Path.GetExtension(updateHeroDto.ImageFile.FileName);
                    var imageName = Guid.NewGuid() + extension;

                    var uploadPath = Path.Combine(resource, "wwwroot", "images", "ProductImage");
                    var saveLocation = Path.Combine(uploadPath, imageName);

                    var oldImage = await GetOldImagControlSelectHero(updateHeroDto.HeroId);
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
                    await updateHeroDto.ImageFile.CopyToAsync(stream);
                    updateHeroDto.ImageUrl = "/images/ProductImage/" + imageName;
                }

                var client = _httpClientFactory.CreateClient();
                var JsonData = JsonConvert.SerializeObject(updateHeroDto);
                var stringContent = new StringContent(JsonData, Encoding.UTF8, "application/json");
                var responseMessage = await client.PutAsync("https://localhost:7287/api/Heroes", stringContent);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("HeroList");
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
            return View(updateHeroDto);
        }

        private async Task<GetHeroByIdDto> GetOldImagControlSelectHero(int heroId)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7287/api/Heroes/GetHeroById?id=" + heroId);

            if (responseMessage.IsSuccessStatusCode)
            {
                var value = await responseMessage.Content.ReadFromJsonAsync<GetHeroByIdDto>();
                return value;
            }
            return new GetHeroByIdDto();
        }
    }
}
