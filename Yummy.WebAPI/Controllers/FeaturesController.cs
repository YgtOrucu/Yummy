using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yummy.WebAPI.Context;
using Yummy.WebAPI.Dtos.FeatureDto;
using Yummy.WebAPI.Entities;

namespace Yummy.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly YummyContext _yummyContext;

        public FeaturesController(IMapper mapper, YummyContext yummyContext)
        {
            _mapper = mapper;
            _yummyContext = yummyContext;
        }

        [HttpGet]
        public async Task<IActionResult> FeatureList()
        {
            var values = await _yummyContext.Features.ToListAsync();
            return Ok(_mapper.Map<List<ResultFeatureDto>>(values));
        }
        [HttpPost]
        public async Task<IActionResult> FeatureCreate(CreateFeatureDto createFeatureDto)
        {
            var values = _mapper.Map<Feature>(createFeatureDto);
            await _yummyContext.Features.AddAsync(values);
            await _yummyContext.SaveChangesAsync();
            return Ok("Ekleme Başarılı");
        }
        [HttpDelete]
        public async Task<IActionResult> FeatureDelete(int id)
        {
            var values = await _yummyContext.Features.FindAsync(id);
            _yummyContext.Features.Remove(values);
            await _yummyContext.SaveChangesAsync();
            return Ok("Silme İşlemi başarılı");
        }
        [HttpPut]
        public async Task<IActionResult> FeatureUpdate(UpdateFeatureDto updateFeatureDto)
        {
            var values = _mapper.Map<Feature>(updateFeatureDto);
            _yummyContext.Features.Update(values);
            await _yummyContext.SaveChangesAsync();
            return Ok("Güncelleme Başarılı");
        }
        [HttpGet("GetFeatureById")]
        public async Task<IActionResult> GetFeatureById(int id)
        {
            var values = await _yummyContext.Features.FindAsync(id);
            return Ok(_mapper.Map<GetFeatureByIdDto>(values));
        }
    }
}
