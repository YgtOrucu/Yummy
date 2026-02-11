namespace Yummy.WebAPI.Dtos.FeatureDto
{
    public class UpdateFeatureDto
    {
        public int FeatureId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Icon { get; set; }
    }
}
