using Elfie.Serialization;
using Microsoft.AspNetCore.Mvc;
using Yummy.WebUI.Dtos.YummyEventsDto;

namespace Yummy.WebUI.Controllers
{
    public class YummyEventsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public YummyEventsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> YummyEventsList()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("YummyClient");
                var responseMessage = await client.GetAsync("YummyEvents");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultYummyEventDto>>();
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
        public IActionResult YummyEventsCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> YummyEventsCreate(CreateYummyEventDto createYummyEventDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {                   
                    return View(createYummyEventDto);
                }

                if (createYummyEventDto.ImageFile != null)
                {
                    var resource = Directory.GetCurrentDirectory();
                    var extension = Path.GetExtension(createYummyEventDto.ImageFile.FileName);
                    var imageName = Guid.NewGuid() + extension;

                    var uploadPath = Path.Combine(resource, "wwwroot", "images", "YummyEventImage");
                    var saveLocation = Path.Combine(uploadPath, imageName);
                    using var stream = new FileStream(saveLocation, FileMode.Create);
                    await createYummyEventDto.ImageFile.CopyToAsync(stream);
                    createYummyEventDto.ImageUrl = "/images/YummyEventImage/" + imageName;
                }

                var client = _httpClientFactory.CreateClient("YummyClient");
                var responseMessage = await client.PostAsJsonAsync("YummyEvents", createYummyEventDto);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("YummyEventsList");
                }
                else
                {
                    return View(createYummyEventDto);
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public async Task<IActionResult> YummyEventsDelete(int id)
        {
            try
            {
                var resource = Directory.GetCurrentDirectory();
                var oldImage = await GetOldImagControlSelectYummyEvents(id);
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
                var responsemessage = await client.DeleteAsync("YummyEvents?id=" + id);

                if (responsemessage.IsSuccessStatusCode)
                    return RedirectToAction("YummyEventsList");

            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> YummyEventsUpdate(int id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("YummyClient");
                var responseMessage = await client.GetAsync("YummyEvents/GetYummyEventById?id=" + id);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var valuess = await responseMessage.Content.ReadFromJsonAsync<UpdateYummyEventDto>();
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
        public async Task<IActionResult> YummyEventsUpdate(UpdateYummyEventDto updateYummyEventDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(updateYummyEventDto);


                if (updateYummyEventDto.ImageFile != null)
                {
                    var resource = Directory.GetCurrentDirectory();
                    var extension = Path.GetExtension(updateYummyEventDto.ImageFile.FileName);
                    var imageName = Guid.NewGuid() + extension;

                    var uploadPath = Path.Combine(resource, "wwwroot", "images", "YummyEventImage");
                    var saveLocation = Path.Combine(uploadPath, imageName);

                    var oldImage = await GetOldImagControlSelectYummyEvents(updateYummyEventDto.YummyEventsId);
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
                    await updateYummyEventDto.ImageFile.CopyToAsync(stream);
                    updateYummyEventDto.ImageUrl = "/images/YummyEventImage/" + imageName;
                }


                var client = _httpClientFactory.CreateClient("YummyClient");
                var responseMessage = await client.PutAsJsonAsync("YummyEvents", updateYummyEventDto);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("YummyEventsList");
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
            return View(updateYummyEventDto);
        }

        private async Task<GetYummyEventByIdDto> GetOldImagControlSelectYummyEvents(int id)
        {
            var client = _httpClientFactory.CreateClient("YummyClient");
            var responseMessage = await client.GetAsync("YummyEvents/GetYummyEventById?id=" + id);

            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<GetYummyEventByIdDto>();
                return values;
            }
            return new GetYummyEventByIdDto();
        }
    }
}
