namespace Yummy.WebUI.Dtos.GalleryDto
{
    public class CreateGalleryDto
    {
        public string? ImageUrl { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string? Title { get; set; }
    }
}
