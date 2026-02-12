namespace Yummy.WebAPI.Dtos.MessageDto
{
    public class ResultMessageDto
    {
        public int MessageId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Subject { get; set; }
        public string? MessageContent { get; set; }
        public DateTime MessageDate { get; set; }
        public bool IsRead { get; set; }
        public string? Status { get; set; }

    }
}
