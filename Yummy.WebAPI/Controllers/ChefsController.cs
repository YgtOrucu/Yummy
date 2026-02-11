using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yummy.WebAPI.Context;
using Yummy.WebAPI.Dtos.ChefDto;
using Yummy.WebAPI.Entities;

namespace Yummy.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChefsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly YummyContext _yummyContext;

        public ChefsController(IMapper mapper, YummyContext yummyContext)
        {
            _mapper = mapper;
            _yummyContext = yummyContext;
        }

        [HttpGet]
        public async Task<IActionResult> ChefList()
        {
            var values = await _yummyContext.Chefs.ToListAsync();
            return Ok(_mapper.Map<List<ResultChefDto>>(values));
        }
        [HttpPost]
        public async Task<IActionResult> ChefCreate(CreateChefDto createChefDto)
        {
            var values = _mapper.Map<Chef>(createChefDto);
            await _yummyContext.Chefs.AddAsync(values);
            await _yummyContext.SaveChangesAsync();
            return Ok("Ekleme Başarılı");
        }
        [HttpDelete]
        public async Task<IActionResult> ChefDelete(int id)
        {
            var values = await _yummyContext.Chefs.FindAsync(id);
            _yummyContext.Chefs.Remove(values);
            await _yummyContext.SaveChangesAsync();
            return Ok("Silme İşlemi başarılı");
        }
        [HttpPut]
        public async Task<IActionResult> ChefUpdate(UpdateChefDto updateChefDto)
        {
            var values = _mapper.Map<Chef>(updateChefDto);
            _yummyContext.Chefs.Update(values);
            await _yummyContext.SaveChangesAsync();
            return Ok("Güncelleme Başarılı");
        }
        [HttpGet("GetChefById")]
        public async Task<IActionResult> GetChefById(int id)
        {
            var values = await _yummyContext.Chefs.FindAsync(id);
            return Ok(_mapper.Map<GetChefByIdDto>(values));
        }
    }
}
