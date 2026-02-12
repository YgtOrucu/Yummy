using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yummy.WebAPI.Context;
using Yummy.WebAPI.Dtos.GalleryDto;
using Yummy.WebAPI.Entities;

namespace Yummy.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GalleriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly YummyContext _yummyContext;

        public GalleriesController(IMapper mapper, YummyContext yummyContext)
        {
            _mapper = mapper;
            _yummyContext = yummyContext;
        }
        [HttpGet]
        public async Task<IActionResult> GalleryList()
        {
            var values = await _yummyContext.Galleries.ToListAsync();
            return Ok(_mapper.Map<List<ResultGalleryDto>>(values));
        }
        [HttpPost]
        public async Task<IActionResult> GalleryCreate(CreateGalleryDto createGalleryDto)
        {
            var values = _mapper.Map<Gallery>(createGalleryDto);
            await _yummyContext.Galleries.AddAsync(values);
            await _yummyContext.SaveChangesAsync();
            return Ok("Ekleme İşlemi Başarılı");
        }
        [HttpDelete]
        public async Task<IActionResult> GalleryDelete(int id)
        {
            var values = await _yummyContext.Galleries.FindAsync(id);
            _yummyContext.Galleries.Remove(values);
            await _yummyContext.SaveChangesAsync();
            return Ok("Silme İşlemi Başarılı");
        }
        [HttpPut]
        public async Task<IActionResult> GalleryUpdate(UpdateGalleryDto updateGalleryDto)
        {
            var values = _mapper.Map<Gallery>(updateGalleryDto);
            _yummyContext.Galleries.Update(values);
            await _yummyContext.SaveChangesAsync();
            return Ok("Güncelleme Başarılı");
        }
        [HttpGet("GetGalleryById")]
        public async Task<IActionResult> GetGalleryById(int id)
        {
            var values = await _yummyContext.Galleries.FindAsync(id);
            return Ok(_mapper.Map<GetGalleryByIdDto>(values));
        }
    }
}
