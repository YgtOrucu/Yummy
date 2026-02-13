using Microsoft.AspNetCore.Mvc;
using Yummy.WebUI.Dtos.TestimonialDto;

namespace Yummy.WebUI.Controllers
{
    public class TestimonialController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TestimonialController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> TestimonialList()
        {
            using (var client = _httpClientFactory.CreateClient("YummyClient"))
            {
                var responseMessage = await client.GetAsync("Testimonials");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultTestimonialDto>>();
                    return View(values);
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult TestimonialCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> TestimonialCreate(CreateTestimonialDto createTestimonialDto)
        {
            if (!ModelState.IsValid)
                return View(createTestimonialDto);

            if (createTestimonialDto.ImageFile != null)
            {
                var resource = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(createTestimonialDto.ImageFile.FileName);
                var imageName = Guid.NewGuid() + extension;

                var uploadPath = Path.Combine(resource, "wwwroot", "images", "TestimonialImage");
                var saveLocation = Path.Combine(uploadPath, imageName);
                using var stream = new FileStream(saveLocation, FileMode.Create);
                await createTestimonialDto.ImageFile.CopyToAsync(stream);
                createTestimonialDto.ImageUrl = "/images/TestimonialImage/" + imageName;
            }

            var client = _httpClientFactory.CreateClient("YummyClient");
            var responseMessage = await client.PostAsJsonAsync("Testimonials", createTestimonialDto);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("TestimonialList");
            }
            else
            {
                return View(createTestimonialDto);
            }
        }


        public async Task<IActionResult> TestimonialDelete(int id)
        {
            try
            {
                var resource = Directory.GetCurrentDirectory();
                var oldImage = await GetOldImagControlSelectTestimonial(id);
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
                var responseMessage = await client.DeleteAsync("Testimonials?id=" + id);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("TestimonialList");
                }
                return View();
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> TestimonialUpdate(int id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("YummyClient");
                var responseMessage = await client.GetAsync("Testimonials/GetTestimonialById?id=" + id);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var values = await responseMessage.Content.ReadFromJsonAsync<UpdateTestimonialDto>();
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
        public async Task<IActionResult> TestimonialUpdate(UpdateTestimonialDto updateTestimonialDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(updateTestimonialDto);
                }

                if (updateTestimonialDto.ImageFile != null)
                {
                    var resource = Directory.GetCurrentDirectory();
                    var extension = Path.GetExtension(updateTestimonialDto.ImageFile.FileName);
                    var imageName = Guid.NewGuid() + extension;

                    var uploadPath = Path.Combine(resource, "wwwroot", "images", "TestimonialImage");
                    var saveLocation = Path.Combine(uploadPath, imageName);

                    var oldImage = await GetOldImagControlSelectTestimonial(updateTestimonialDto.TestimonialId);
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
                    await updateTestimonialDto.ImageFile.CopyToAsync(stream);
                    updateTestimonialDto.ImageUrl = "/images/TestimonialImage/" + imageName;
                }
                var client = _httpClientFactory.CreateClient("YummyClient");
                var responseMessage = await client.PutAsJsonAsync("Testimonials", updateTestimonialDto);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("TestimonialList");
                }
                return View(updateTestimonialDto);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        private async Task<GetTestimonialByIdDto> GetOldImagControlSelectTestimonial(int TestimonialId)
        {
            var client = _httpClientFactory.CreateClient("YummyClient");
            var responseMessage = await client.GetAsync("Testimonials/GetTestimonialById?id=" + TestimonialId);

            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<GetTestimonialByIdDto>();
                return values;
            }
            return new GetTestimonialByIdDto();
        }
    }
}
