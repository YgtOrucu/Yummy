namespace Yummy.WebUI.Dtos.YummyEventsDto
{
    public class GetYummyEventByIdDto
    {
        public int YummyEventsId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
