using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yummy.WebAPI.Context;
using Yummy.WebAPI.Dtos.MessageDto;
using Yummy.WebAPI.Dtos.MessageDto.MessageDtoForAdminThema.MessageListForNavbarSection;
using Yummy.WebAPI.Entities;

namespace Yummy.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly YummyContext _yummyContext;

        public MessagesController(IMapper mapper, YummyContext yummyContext)
        {
            _mapper = mapper;
            _yummyContext = yummyContext;
        }

        [HttpGet]
        public async Task<IActionResult> MessageList()
        {
            var values = await _yummyContext.Messages.ToListAsync();
            return Ok(_mapper.Map<List<ResultMessageDto>>(values));
        }
        [HttpPost]
        public async Task<IActionResult> MessageCreate(CreateMessageDto createMessageDto)
        {
            var values = _mapper.Map<Message>(createMessageDto);
            await _yummyContext.Messages.AddAsync(values);
            await _yummyContext.SaveChangesAsync();
            return Ok("Ekleme Başarılı");
        }
        [HttpDelete]
        public async Task<IActionResult> MessageDelete(int id)
        {
            var values = await _yummyContext.Messages.FindAsync(id);
            _yummyContext.Messages.Remove(values);
            await _yummyContext.SaveChangesAsync();
            return Ok("Silme İşlemi başarılı");
        }
        [HttpPut]
        public async Task<IActionResult> MessageUpdate(UpdateMessageDto updateMessageDto)
        {
            var values = _mapper.Map<Message>(updateMessageDto);
            _yummyContext.Messages.Update(values);
            await _yummyContext.SaveChangesAsync();
            return Ok("Güncelleme Başarılı");
        }
        [HttpGet("GetMessageById")]
        public async Task<IActionResult> GetMessageById(int id)
        {
            var values = await _yummyContext.Messages.FindAsync(id);
            return Ok(_mapper.Map<GetMessageByIdDto>(values));
        }
        [HttpGet("MessageListByIsReadFalse")]
        public async Task<IActionResult> MessageListByIsReadFalse()
        {
            var values = await _yummyContext.Messages.Where(x => !x.IsRead).ToListAsync();
            return Ok(_mapper.Map<List<MessageListByIsReadFalseDto>>(values));
        }
    }
}
