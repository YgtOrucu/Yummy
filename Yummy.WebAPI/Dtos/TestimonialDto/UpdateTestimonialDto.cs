namespace Yummy.WebAPI.Dtos.TestimonialDto
{
    public class UpdateTestimonialDto
    {
        public int TestimonialId { get; set; }
        public string? Name { get; set; }
        public string? Title { get; set; }
        public string? Comment { get; set; }
        public string? ImageUrl { get; set; }
        public int Rating { get; set; }
    }
}
