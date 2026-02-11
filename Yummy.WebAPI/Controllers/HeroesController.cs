using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yummy.WebAPI.Context;
using Yummy.WebAPI.Dtos.HeroDto;
using Yummy.WebAPI.Entities;

namespace Yummy.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly YummyContext _yummyContext;

        public HeroesController(IMapper mapper, YummyContext yummyContext)
        {
            _mapper = mapper;
            _yummyContext = yummyContext;
        }

        [HttpGet]
        public async Task<IActionResult> HeroList()
        {
            var values =  await _yummyContext.Heroes.ToListAsync();
            return Ok(_mapper.Map<List<ResultHeroDto>>(values));
        }

        [HttpPost]
        public async Task<IActionResult> HeroCreate(CreateHeroDto createHeroDto)
        {
            var values = _mapper.Map<Hero>(createHeroDto);
            _yummyContext.Heroes.AddAsync(values);
            await _yummyContext.SaveChangesAsync();
            return Ok("Ekleme İşlemi başarılı");
        }
        [HttpDelete]
        public async Task<IActionResult> HeroDelete(int id)
        {
            var values = await _yummyContext.Heroes.FindAsync(id);
            _yummyContext.Heroes.Remove(values);
            await _yummyContext.SaveChangesAsync();
            return Ok("Silme Başarılı");
        }

        [HttpPut]
        public async Task<IActionResult> HeroUpdate(UpdateHeroDto updateHeroDto)
        {
            var values = _mapper.Map<Hero>(updateHeroDto);
            _yummyContext.Heroes.Update(values);
            await _yummyContext.SaveChangesAsync();
            return Ok("Güncelleme Başarılı");
        }
        [HttpGet("GetHeroById")]
        public async Task<IActionResult> GetHeroById(int id)
        {
            var values = await _yummyContext.Heroes.FindAsync(id);
            return Ok(_mapper.Map<GetHeroByIdDto>(values));
        }
    }
}
