using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;
using Yummy.WebUI.Dtos.CategoryDto;
using Yummy.WebUI.Dtos.ProductDto;

namespace Yummy.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        private async Task<List<SelectListItem>> GetCategoriesForDropdown()
        {
            var client = _httpClientFactory.CreateClient("YummyClient");
            var responseMessage = await client.GetAsync("Categories");

            if (responseMessage.IsSuccessStatusCode)
            {
                var categories = await responseMessage.Content.ReadFromJsonAsync<List<ResultCategoryDto>>();

                return categories.Select(x => new SelectListItem
                {
                    Value = x.CategoryId.ToString(),
                    Text = x.CategoryName
                }).ToList();
            }
            return new List<SelectListItem>();
        }

        public async Task<IActionResult> ProductList()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("YummyClient");
                var responseMessage = await client.GetAsync("Products");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductDto>>();
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
        public async Task<IActionResult> ProductCreate()
        {
            var categoryList = await GetCategoriesForDropdown();
            ViewBag.CategoryList = categoryList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProductCreate(CreateProductDto createProductDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var categoryList = await GetCategoriesForDropdown();
                    ViewBag.CategoryList = categoryList;
                    return View(createProductDto);
                }

                if (createProductDto.ImageFile != null)
                {
                    var resource = Directory.GetCurrentDirectory();
                    var extension = Path.GetExtension(createProductDto.ImageFile.FileName);
                    var imageName = Guid.NewGuid() + extension;

                    var uploadPath = Path.Combine(resource, "wwwroot", "images", "ProductImage");
                    var saveLocation = Path.Combine(uploadPath, imageName);
                    using var stream = new FileStream(saveLocation, FileMode.Create);
                    await createProductDto.ImageFile.CopyToAsync(stream);
                    createProductDto.ImageUrl = "/images/ProductImage/" + imageName;
                }

                var client = _httpClientFactory.CreateClient("YummyClient");
                var responseMessage = await client.PostAsJsonAsync("Products", createProductDto);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("ProductList");
                }
                else
                {
                    var categoryList = await GetCategoriesForDropdown();
                    ViewBag.CategoryList = categoryList;
                    return View(createProductDto);
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public async Task<IActionResult> ProductDelete(int id)
        {
            try
            {
                var resource = Directory.GetCurrentDirectory();
                var oldImage = await GetOldImagControlSelectProduct(id);
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
                var responseMessage = await client.DeleteAsync("Products?id=" + id);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("ProductList");
                }
                return View();
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ProductUpdate(int id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("YummyClient");
                var responseMessage = await client.GetAsync("Products/GetProductById?id=" + id);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var values = await responseMessage.Content.ReadFromJsonAsync<UpdateProductDto>();
                    var getCategory = await client.GetAsync("Categories");
                    var categories = await getCategory.Content.ReadFromJsonAsync<List<ResultCategoryDto>>();

                    List<SelectListItem> selectListItems = (from x in categories
                                                            select new SelectListItem
                                                            {
                                                                Value = x.CategoryId.ToString(),
                                                                Text = x.CategoryName,
                                                                Selected = x.CategoryId == values.CategoryId
                                                            }).ToList();
                    ViewBag.CategoryList = selectListItems;
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
        public async Task<IActionResult> ProductUpdate(UpdateProductDto updateProductDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(updateProductDto);
                }

                if (updateProductDto.ImageFile != null)
                {
                    var resource = Directory.GetCurrentDirectory();
                    var extension = Path.GetExtension(updateProductDto.ImageFile.FileName);
                    var imageName = Guid.NewGuid() + extension;

                    var uploadPath = Path.Combine(resource, "wwwroot", "images", "ProductImage");
                    var saveLocation = Path.Combine(uploadPath, imageName);

                    var oldImage = await GetOldImagControlSelectProduct(updateProductDto.ProductId);
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
                    await updateProductDto.ImageFile.CopyToAsync(stream);
                    updateProductDto.ImageUrl = "/images/ProductImage/" + imageName;
                }
                var client = _httpClientFactory.CreateClient("YummyClient");
                var responseMessage = await client.PutAsJsonAsync("Products", updateProductDto);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("ProductList");
                }
                return View(updateProductDto);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        private async Task<GetProductByIdDto> GetOldImagControlSelectProduct(int productId)
        {
            var client = _httpClientFactory.CreateClient("YummyClient");
            var responseMessage = await client.GetAsync("Products/GetProductById?id=" + productId);

            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<GetProductByIdDto>();
                return values;
            }
            return new GetProductByIdDto();
        }
    }
}
