using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yummy.WebAPI.Context;
using Yummy.WebAPI.Dtos.CategoryDto;
using Yummy.WebAPI.Entities;

namespace Yummy.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly YummyContext _yummyContext;

        public CategoriesController(IMapper mapper, YummyContext yummyContext)
        {
            _mapper = mapper;
            _yummyContext = yummyContext;
        }

        [HttpGet]
        public async Task<IActionResult> CategoryList()
        {
            var values = await _yummyContext.Categories.ToListAsync();
            return Ok(_mapper.Map<List<ResultCategoryDto>>(values));
        }
        [HttpPost]
        public async Task<IActionResult> CategoryCreate(CreateCategoryDto createCategoryDto)
        {
            var values = _mapper.Map<Category>(createCategoryDto);
            await _yummyContext.Categories.AddAsync(values);
            await _yummyContext.SaveChangesAsync();
            return Ok("Ekleme Başarılı");
        }
        [HttpDelete]
        public async Task<IActionResult> CategoryDelete(int id)
        {
            var values = await _yummyContext.Categories.FindAsync(id);
            _yummyContext.Categories.Remove(values);
            await _yummyContext.SaveChangesAsync();
            return Ok("Silme İşlemi başarılı");
        }
        [HttpPut]
        public async Task<IActionResult> CategoryUpdate(UpdateCategoryDto updateCategoryDto)
        {
            var values = _mapper.Map<Category>(updateCategoryDto);
            _yummyContext.Categories.Update(values);
            await _yummyContext.SaveChangesAsync();
            return Ok("Güncelleme Başarılı");
        }
        [HttpGet("GetCategoryById")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var values = await _yummyContext.Categories.FindAsync(id);
            return Ok(_mapper.Map<GetCategoryByIdDto>(values));
        }
    }
}
