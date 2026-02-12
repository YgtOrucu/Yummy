using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yummy.WebAPI.Context;
using Yummy.WebAPI.Dtos.ReservationDto;
using Yummy.WebAPI.Entities;

namespace Yummy.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly YummyContext _yummyContext;

        public ReservationsController(IMapper mapper, YummyContext yummyContext)
        {
            _mapper = mapper;
            _yummyContext = yummyContext;
        }
        [HttpGet]
        public async Task<IActionResult> ReservationList()
        {
            var values = await _yummyContext.Reservations.ToListAsync();
            return Ok(_mapper.Map<List<ResultReservationDto>>(values));
        }
        [HttpPost]
        public async Task<IActionResult> ReservationCreate(CreateReservationDto createReservationDto)
        {
            var values = _mapper.Map<Reservation>(createReservationDto);
            await _yummyContext.Reservations.AddAsync(values);
            await _yummyContext.SaveChangesAsync();
            return Ok("Ekleme İşlemi Başarılı");
        }
        [HttpDelete]
        public async Task<IActionResult> ReservationDelete(int id)
        {
            var values = await _yummyContext.Reservations.FindAsync(id);
            _yummyContext.Reservations.Remove(values);
            await _yummyContext.SaveChangesAsync();
            return Ok("Silme İşlemi Başarılı");
        }
        [HttpPut]
        public async Task<IActionResult> ReservationUpdate(UpdateReservationDto updateReservationDto)
        {
            var values = _mapper.Map<Reservation>(updateReservationDto);
            _yummyContext.Reservations.Update(values);
            await _yummyContext.SaveChangesAsync();
            return Ok("Güncelleme Başarılı");
        }
        [HttpGet("GetReservationById")]
        public async Task<IActionResult> GetReservationById(int id)
        {
            var values = await _yummyContext.Reservations.FindAsync(id);
            return Ok(_mapper.Map<GetReservationByIdDto>(values));
        }
    }
}
