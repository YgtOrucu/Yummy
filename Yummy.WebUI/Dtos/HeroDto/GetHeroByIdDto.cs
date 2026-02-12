namespace Yummy.WebUI.Dtos.HeroDto
{
    public class GetHeroByIdDto
    {
        public int HeroId { get; set; }
        public string? Title { get; set; }
        public string? SubTitle { get; set; }
        public string? Description { get; set; }
        public string? VideoUrl { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
