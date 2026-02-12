using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yummy.WebAPI.Context;
using Yummy.WebAPI.Dtos.ContactDto;
using Yummy.WebAPI.Entities;

namespace Yummy.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly YummyContext _yummyContext;

        public ContactsController(IMapper mapper, YummyContext yummyContext)
        {
            _mapper = mapper;
            _yummyContext = yummyContext;
        }

        [HttpGet]
        public async Task<IActionResult> ContactList()
        {
            var values = await _yummyContext.Contacts.ToListAsync();
            return Ok(_mapper.Map<List<ResultContactDto>>(values));
        }
        [HttpPost]
        public async Task<IActionResult> ContactCreate(CreateContactDto createContactDto)
        {
            var values = _mapper.Map<Contact>(createContactDto);
            await _yummyContext.Contacts.AddAsync(values);
            await _yummyContext.SaveChangesAsync();
            return Ok("Ekleme Başarılı");
        }
        [HttpDelete]
        public async Task<IActionResult> ContactDelete(int id)
        {
            var values = await _yummyContext.Contacts.FindAsync(id);
            _yummyContext.Contacts.Remove(values);
            await _yummyContext.SaveChangesAsync();
            return Ok("Silme İşlemi başarılı");
        }
        [HttpPut]
        public async Task<IActionResult> ContactUpdate(UpdateContactDto updateContactDto)
        {
            var values = _mapper.Map<Contact>(updateContactDto);
            _yummyContext.Contacts.Update(values);
            await _yummyContext.SaveChangesAsync();
            return Ok("Güncelleme Başarılı");
        }
        [HttpGet("GetContactById")]
        public async Task<IActionResult> GetContactById(int id)
        {
            var values = await _yummyContext.Contacts.FindAsync(id);
            return Ok(_mapper.Map<GetContactByIdDto>(values));
        }
    }
}
