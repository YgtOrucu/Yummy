namespace Yummy.WebUI.Dtos.YummyEventsDto
{
    public class CreateYummyEventDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
