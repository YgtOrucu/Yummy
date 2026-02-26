namespace Yummy.WebAPI.Dtos.AboutDto
{

    public class CreateAboutDto
    {
        public string? Title { get; set; }
        public string? ImageUrl { get; set; }
        public string? VideoCoverImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public string? Description { get; set; }
        public string? ListDescription1 { get; set; }
        public string? ListDescription2 { get; set; }
        public string? ListDescription3 { get; set; }
        public string? ReservationNumber { get; set; }
    }

}
