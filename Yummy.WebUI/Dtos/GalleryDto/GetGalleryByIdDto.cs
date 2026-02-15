namespace Yummy.WebUI.Dtos.GalleryDto
{
    public class GetGalleryByIdDto
    {
        public int GalleryId { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string? Title { get; set; }
    }
}
