namespace Yummy.WebUI.Dtos.TestimonialDto
{
    public class CreateTestimonialDto
    {
        public string? Name { get; set; }
        public string? Title { get; set; }
        public string? Comment { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? ImageFile { get; set; }
        public int Rating { get; set; }
    }
}
