using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yummy.WebAPI.Context;
using Yummy.WebAPI.Dtos.ProductDto;
using Yummy.WebAPI.Entities;

namespace Yummy.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly YummyContext _yummyContext;

        public ProductsController(IMapper mapper, YummyContext yummyContext)
        {
            _mapper = mapper;
            _yummyContext = yummyContext;
        }

        [HttpGet]
        public async Task<IActionResult> ProductList()
        {
            var values = await _yummyContext.Products.Include(x => x.Category).ToListAsync();
            return Ok(_mapper.Map<List<ResultProductDto>>(values));
        }
        [HttpPost]
        public async Task<IActionResult> ProductCreate(CreateProductDto createProductDto)
        {
            var values = _mapper.Map<Product>(createProductDto);
            await _yummyContext.Products.AddAsync(values);
            await _yummyContext.SaveChangesAsync();
            return Ok("Ekleme Başarılı");
        }
        [HttpDelete]
        public async Task<IActionResult> ProductDelete(int id)
        {
            var values = await _yummyContext.Products.FindAsync(id);
            _yummyContext.Products.Remove(values);
            await _yummyContext.SaveChangesAsync();
            return Ok("Silme Başarılı");
        }

        [HttpPut]
        public async Task<IActionResult> ProductUpdate(UpdateProductDto updateProductDto)
        {
            var values = _mapper.Map<Product>(updateProductDto);
            _yummyContext.Products.Update(values);
            await _yummyContext.SaveChangesAsync();
            return Ok("Güncelleme Başarılı");
        }
        [HttpGet("GetProductById")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var values = await _yummyContext.Products.FindAsync(id);
            return Ok(_mapper.Map<GetProductByIdDto>(values));
        }
    }
}
