using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yummy.WebAPI.Context;
using Yummy.WebAPI.Dtos.AboutDto;
using Yummy.WebAPI.Entities;

namespace Yummy.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly YummyContext _yummyContext;

        public AboutsController(IMapper mapper, YummyContext yummyContext)
        {
            _mapper = mapper;
            _yummyContext = yummyContext;
        }

        [HttpGet]
        public async Task<IActionResult> AboutList()
        {
            var values =  await _yummyContext.Abouts.ToListAsync();
            return Ok(_mapper.Map<List<ResultAboutDto>>(values));
        }
        [HttpPost]
        public async Task<IActionResult> AboutCreate(CreateAboutDto createAboutDto)
        {
            var values = _mapper.Map<About>(createAboutDto);
            await _yummyContext.Abouts.AddAsync(values);
            await _yummyContext.SaveChangesAsync();
            return Ok("Ekleme işlemi başarılı");
        }
        [HttpDelete]
        public async Task<IActionResult> AboutDelete(int id)
        {
            var values = await _yummyContext.Abouts.FindAsync(id);
            _yummyContext.Abouts.Remove(values);
            await _yummyContext.SaveChangesAsync();
            return Ok("Silme İşlemi başarılı");
        }
        [HttpPut]
        public async Task<IActionResult> AboutUpdate(UpdateAboutDto updateAboutDto)
        {
            var values = _mapper.Map<About>(updateAboutDto);
            _yummyContext.Abouts.Update(values);
            await _yummyContext.SaveChangesAsync();
            return Ok("Güncelleme işlemi başarılı");
        }

        [HttpGet("GetAboutById")]
        public async Task<IActionResult> GetAboutById(int id)
        {
            var values = await _yummyContext.Abouts.FindAsync(id);
            return Ok(_mapper.Map<GetAboutByIdDto>(values));
        }
    }
}
