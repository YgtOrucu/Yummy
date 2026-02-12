namespace Yummy.WebUI.Dtos.ChefDto
{
    public class UpdateChefDto
    {
        public int ChefId { get; set; }
        public string? Name { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
    }
}
