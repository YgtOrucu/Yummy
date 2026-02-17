namespace Yummy.WebAPI.Dtos.DashboardDto
{
    public class ResultGetChef
    {
        public string Name { get; set; }
        public string Title { get; set; } 
        public string Status { get; set; }
        public string? ImageUrl { get; set; }
        public int TaskCount { get; set; }

    }
}
