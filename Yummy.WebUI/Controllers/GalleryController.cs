using Microsoft.AspNetCore.Mvc;
using Yummy.WebUI.Dtos.GalleryDto;

namespace Yummy.WebUI.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GalleryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
       
        public async Task<IActionResult> GalleryList()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("YummyClient");
                var responseMessage = await client.GetAsync("Galleries");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultGalleryDto>>();
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
        public IActionResult GalleryCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GalleryCreate(CreateGalleryDto createGalleryDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(createGalleryDto);
                }

                if (createGalleryDto.ImageFile != null)
                {
                    var resource = Directory.GetCurrentDirectory();
                    var extension = Path.GetExtension(createGalleryDto.ImageFile.FileName);
                    var imageName = Guid.NewGuid() + extension;

                    var uploadPath = Path.Combine(resource, "wwwroot", "images", "GalleryImage");
                    var saveLocation = Path.Combine(uploadPath, imageName);
                    using var stream = new FileStream(saveLocation, FileMode.Create);
                    await createGalleryDto.ImageFile.CopyToAsync(stream);
                    createGalleryDto.ImageUrl = "/images/GalleryImage/" + imageName;
                }

                var client = _httpClientFactory.CreateClient("YummyClient");
                var responseMessage = await client.PostAsJsonAsync("Galleries", createGalleryDto);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("GalleryList");
                }
                else
                {
                    return View(createGalleryDto);
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public async Task<IActionResult> GalleryDelete(int id)
        {
            try
            {
                var resource = Directory.GetCurrentDirectory();
                var oldImage = await GetOldImagControlSelectGallery(id);
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
                var responseMessage = await client.DeleteAsync("Galleries?id=" + id);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("GalleryList");
                }
                return View();
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GalleryUpdate(int id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("YummyClient");
                var responseMessage = await client.GetAsync("Galleries/GetGalleryById?id=" + id);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var values = await responseMessage.Content.ReadFromJsonAsync<UpdateGalleryDto>();
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
        public async Task<IActionResult> GalleryUpdate(UpdateGalleryDto updateGalleryDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(updateGalleryDto);
                }

                if (updateGalleryDto.ImageFile != null)
                {
                    var resource = Directory.GetCurrentDirectory();
                    var extension = Path.GetExtension(updateGalleryDto.ImageFile.FileName);
                    var imageName = Guid.NewGuid() + extension;

                    var uploadPath = Path.Combine(resource, "wwwroot", "images", "GalleryImage");
                    var saveLocation = Path.Combine(uploadPath, imageName);

                    var oldImage = await GetOldImagControlSelectGallery(updateGalleryDto.GalleryId);
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
                    await updateGalleryDto.ImageFile.CopyToAsync(stream);
                    updateGalleryDto.ImageUrl = "/images/GalleryImage/" + imageName;
                }
                var client = _httpClientFactory.CreateClient("YummyClient");
                var responseMessage = await client.PutAsJsonAsync("Galleries", updateGalleryDto);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("GalleryList");
                }
                return View(updateGalleryDto);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        private async Task<GetGalleryByIdDto> GetOldImagControlSelectGallery(int GalleryId)
        {
            var client = _httpClientFactory.CreateClient("YummyClient");
            var responseMessage = await client.GetAsync("Galleries/GetGalleryById?id=" + GalleryId);

            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<GetGalleryByIdDto>();
                return values;
            }
            return new GetGalleryByIdDto();
        }
    }
}
