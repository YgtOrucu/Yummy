namespace Yummy.WebAPI.Dtos.DashboardDto
{
    public class ResultGetMessage
    {
        public int MessageId { get; set; }
        public string? Name { get; set; }
        public string? Subject { get; set; }
        public string? MessageContent { get; set; }
        public DateTime MessageDate { get; set; }
        public string? Status { get; set; }
    }
}
