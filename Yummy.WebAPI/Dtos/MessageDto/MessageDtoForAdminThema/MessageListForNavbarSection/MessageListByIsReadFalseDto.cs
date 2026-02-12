namespace Yummy.WebAPI.Dtos.MessageDto.MessageDtoForAdminThema.MessageListForNavbarSection
{
    public class MessageListByIsReadFalseDto
    {
        public int MessageId { get; set; }
        public string? Name { get; set; }
        public string? Subject { get; set; }
        public DateTime MessageDate { get; set; }
    }
}
