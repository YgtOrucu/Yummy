using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yummy.WebAPI.Context;
using Yummy.WebAPI.Dtos.TestimonialDto;
using Yummy.WebAPI.Entities;

namespace Yummy.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly YummyContext _yummyContext;

        public TestimonialsController(IMapper mapper, YummyContext yummyContext)
        {
            _mapper = mapper;
            _yummyContext = yummyContext;
        }

        [HttpGet]
        public async Task<IActionResult> TestimonialList()
        {
            var values = await _yummyContext.Testimonials.ToListAsync();
            return Ok(_mapper.Map<List<ResultTestimonialDto>>(values));
        }
        [HttpPost]
        public async Task<IActionResult> TestimonialCreate(CreateTestimonialDto createTestimonialDto)
        {
            var values = _mapper.Map<Testimonial>(createTestimonialDto);
            await _yummyContext.Testimonials.AddAsync(values);
            await _yummyContext.SaveChangesAsync();
            return Ok("Ekleme Başarılı");
        }
        [HttpDelete]
        public async Task<IActionResult> TestimonialDelete(int id)
        {
            var values = await _yummyContext.Testimonials.FindAsync(id);
            _yummyContext.Testimonials.Remove(values);
            await _yummyContext.SaveChangesAsync();
            return Ok("Silme İşlemi başarılı");
        }
        [HttpPut]
        public async Task<IActionResult> TestimonialUpdate(UpdateTestimonialDto updateTestimonialDto)
        {
            var values = _mapper.Map<Testimonial>(updateTestimonialDto);
            _yummyContext.Testimonials.Update(values);
            await _yummyContext.SaveChangesAsync();
            return Ok("Güncelleme Başarılı");
        }
        [HttpGet("GetTestimonialById")]
        public async Task<IActionResult> GetTestimonialById(int id)
        {
            var values = await _yummyContext.Testimonials.FindAsync(id);
            return Ok(_mapper.Map<GetTestimonialByIdDto>(values));
        }
    }
}
