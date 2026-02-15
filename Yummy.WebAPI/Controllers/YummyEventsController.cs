using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yummy.WebAPI.Context;
using Yummy.WebAPI.Dtos.YummyEventsDto;
using Yummy.WebAPI.Entities;

namespace Yummy.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YummyEventsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly YummyContext _yummyContext;

        public YummyEventsController(IMapper mapper, YummyContext yummyContext)
        {
            _mapper = mapper;
            _yummyContext = yummyContext;
        }

        [HttpGet]
        public async Task<IActionResult> YummyEventList()
        {
            var values = await _yummyContext.YummyEvents.ToListAsync();
            return Ok(_mapper.Map<List<ResultYummyEventDto>>(values));
        }
        [HttpPost]
        public async Task<IActionResult> YummyEventCreate(CreateYummyEventDto createYummyEventDto)
        {
            var values = _mapper.Map<YummyEvents>(createYummyEventDto);
            await _yummyContext.YummyEvents.AddAsync(values);
            await _yummyContext.SaveChangesAsync();
            return Ok("Ekleme işlemi başarılı");
        }
        [HttpDelete]
        public async Task<IActionResult> YummyEventDelete(int id)
        {
            var values = await _yummyContext.YummyEvents.FindAsync(id);
            _yummyContext.YummyEvents.Remove(values);
            await _yummyContext.SaveChangesAsync();
            return Ok("Silme İşlemi başarılı");
        }
        [HttpPut]
        public async Task<IActionResult> YummyEventUpdate(UpdateYummyEventDto updateYummyEventDto)
        {
            var values = _mapper.Map<YummyEvents>(updateYummyEventDto);
            _yummyContext.YummyEvents.Update(values);
            await _yummyContext.SaveChangesAsync();
            return Ok("Güncelleme işlemi başarılı");
        }

        [HttpGet("GetYummyEventById")]
        public async Task<IActionResult> GetYummyEventById(int id)
        {
            var values = await _yummyContext.YummyEvents.FindAsync(id);
            return Ok(_mapper.Map<GetYummyEventByIdDto>(values));
        }
    }
}
