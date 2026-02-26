using Microsoft.AspNetCore.Mvc;
using Yummy.WebUI.Dtos.AboutDto;

namespace Yummy.WebUI.Controllers
{
    public class AboutController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AboutController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> AboutList()
        {
            try
            {
                using(var client = _httpClientFactory.CreateClient("YummyClient"))
                {
                    var responseMessage = await client.GetAsync("Abouts");

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultAboutDto>>();
                        return View(values);
                    }
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
            return View();
        }

        [HttpGet]
        public IActionResult AboutCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AboutCreate(CreateAboutDto createAboutDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(createAboutDto);

                if (createAboutDto.ImageFileForImageUrl != null)
                {
                    var resource = Directory.GetCurrentDirectory();
                    var extension = Path.Combine(createAboutDto.ImageFileForImageUrl.FileName);
                    var imageName = Guid.NewGuid() + extension;

                    var uploadPath = Path.Combine(resource, "wwwroot", "images", "AboutImage");
                    var saveLocation = Path.Combine(uploadPath, imageName);
                    using var stream = new FileStream(saveLocation, FileMode.Create);
                    await createAboutDto.ImageFileForImageUrl.CopyToAsync(stream);
                    createAboutDto.ImageUrl = "/images/AboutImage/" + imageName;
                }

                if (createAboutDto.ImageFileForVideoCover != null)
                {
                    var resource = Directory.GetCurrentDirectory();
                    var extension = Path.Combine(createAboutDto.ImageFileForVideoCover.FileName);
                    var imageName = Guid.NewGuid() + extension;

                    var uploadPath = Path.Combine(resource, "wwwroot", "images", "AboutImage");
                    var saveLocation = Path.Combine(uploadPath, imageName);
                    using var stream = new FileStream(saveLocation, FileMode.Create);
                    await createAboutDto.ImageFileForVideoCover.CopyToAsync(stream);
                    createAboutDto.VideoCoverImageUrl = "/images/AboutImage/" + imageName;
                }
                var client = _httpClientFactory.CreateClient("YummyClient");
                var responseMessage = await client.PostAsJsonAsync("Abouts", createAboutDto);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("AboutList");
                }
                else
                {
                    return View(createAboutDto);
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }  
        }

        public async Task<IActionResult> AboutDelete(int id)
        {
            try
            {
                var resource = Directory.GetCurrentDirectory();
                var oldImage = await GetOldImagControlSelectAbout(id);
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
                if (!string.IsNullOrEmpty(oldImage.VideoCoverImageUrl))
                {
                    var oldImagePath = Path.Combine(
                        resource,
                        "wwwroot",
                        oldImage.VideoCoverImageUrl.TrimStart('/')
                    );

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                var client = _httpClientFactory.CreateClient("YummyClient");
                var responseMessage = await client.DeleteAsync("Abouts?id=" + id);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("AboutList");
                }
                return View();
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> AboutUpdate(int id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("YummyClient");
                var responseMessage = await client.GetAsync("Abouts/GetAboutById?id=" + id);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var values = await responseMessage.Content.ReadFromJsonAsync<UpdateAboutDto>();
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
        public async Task<IActionResult> AboutUpdate(UpdateAboutDto updateAboutDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(updateAboutDto);
                }

                var oldImage = await GetOldImagControlSelectAbout(updateAboutDto.AboutId);

                if (updateAboutDto.ImageFileForImageUrl != null)
                {
                    var resource = Directory.GetCurrentDirectory();
                    var extension = Path.GetExtension(updateAboutDto.ImageFileForImageUrl.FileName);
                    var imageName = Guid.NewGuid() + extension;

                    var uploadPath = Path.Combine(resource, "wwwroot", "images", "AboutImage");
                    var saveLocation = Path.Combine(uploadPath, imageName);

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
                    await updateAboutDto.ImageFileForImageUrl.CopyToAsync(stream);
                    updateAboutDto.ImageUrl = "/images/AboutImage/" + imageName;
                }
                else
                {
                    updateAboutDto.ImageUrl = oldImage.ImageUrl;
                }

                if (updateAboutDto.ImageFileForVideoCover != null)
                {
                    var resource = Directory.GetCurrentDirectory();
                    var extension = Path.GetExtension(updateAboutDto.ImageFileForVideoCover.FileName);
                    var imageName = Guid.NewGuid() + extension;

                    var uploadPath = Path.Combine(resource, "wwwroot", "images", "AboutImage");
                    var saveLocation = Path.Combine(uploadPath, imageName);
                    if (!string.IsNullOrEmpty(oldImage.VideoCoverImageUrl))
                    {
                        var oldImagePath = Path.Combine(
                            resource,
                            "wwwroot",
                            oldImage.VideoCoverImageUrl.TrimStart('/')
                        );

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using var stream = new FileStream(saveLocation, FileMode.Create);
                    await updateAboutDto.ImageFileForVideoCover.CopyToAsync(stream);
                    updateAboutDto.VideoCoverImageUrl = "/images/AboutImage/" + imageName;
                }
                else
                {
                    updateAboutDto.VideoCoverImageUrl = oldImage.VideoCoverImageUrl;
                }
                var client = _httpClientFactory.CreateClient("YummyClient");  
                var responseMessage = await client.PutAsJsonAsync("Abouts", updateAboutDto);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("AboutList");
                }
                return View(updateAboutDto);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        private async Task<GetAboutByIdDto> GetOldImagControlSelectAbout(int aboutıd)
        {
            var client = _httpClientFactory.CreateClient("YummyClient");
            var responseMessage = await client.GetAsync("Abouts/GetAboutById?id=" + aboutıd);

            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<GetAboutByIdDto>();
                return values;
            }
            return new GetAboutByIdDto();
        }
    }
}
